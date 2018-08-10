using GradeProject.ProfileService.Communication.CommandHandlers;
using GradeProject.ProfileService.Communication.Commands;
using GradeProject.ProfileService.Config;
using GradeProject.ProfileService.Infrastructure;
using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using GradeProject.ProfileService.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Communication
{
    public class RabbitMqBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;

        //Gueues
        private readonly string _gameRegisteredQueue;

        //Dependencies
        private readonly ICommandBus _commandBus;

        public RabbitMqBus(IOptions<RabbitMqConfig> config, ICommandBus commandBus)
        {
            _commandBus = commandBus;

            var connFactory = new ConnectionFactory() { HostName = config.Value.HostName };
            _connection = connFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(config.Value.Exchange, "direct");

            _gameRegisteredQueue = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(_gameRegisteredQueue, config.Value.Exchange, "register");

            _consumer = new EventingBasicConsumer(_channel);

            RegisterConsumers();
        }

        public void RegisterConsumers()
        {
            //Dependinc on number of consumers
            _consumer.Received += async (model, ea) =>
            {
                var userInfo = ObjectConverter.ByteToObject<UserInsertDTO>(ea.Body);
                await _commandBus.SubmitAsync(new AddProfileCommand(userInfo));
            };

            _channel.BasicConsume(_gameRegisteredQueue, true, _consumer);
        }

        public void Register(string routingKey, AddProfileCommand command)
        {
            //If it get complex I will add some routingKey switch
            // and work with ICommand
            _consumer.Received += async (model, ea) =>
             {
                 var userInfo = ObjectConverter.ByteToObject<UserInsertDTO>(ea.Body);

                 command.UserInfo = userInfo;
                 await _commandBus.SubmitAsync(command);
             };
        }

        public void Publish(byte[] data)
        {
            _channel.BasicPublish(exchange: "",
                                  routingKey: "",
                                  basicProperties: null,
                                  body: data);
        }
    }

}
