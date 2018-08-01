using GradeProject.CommandBusInterfaces;
using GradeProject.GameCatalogService;
using GradeProject.GameCatalogService.Communication.Commands;
using GradeProject.GameCatalogService.Configurations;
using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Infrastructure.Services;
using GradeProject.GameCatalogService.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Communication
{
    public class RabbitMqManager : IRabbitMqManager
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;

        //Gueues
        private readonly string _gameRegisteredQueue;

        //Dependencies
        private readonly ICommandBus _commandBus;

        public RabbitMqManager(IOptions<RabbitMqConfig> config, ICommandBus commandBus)
        {
            _commandBus = commandBus;

            var connFactory = new ConnectionFactory() { HostName = config.Value.HostName };
            _connection = connFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(config.Value.Exchange, "direct");

            _gameRegisteredQueue = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(_gameRegisteredQueue, config.Value.Exchange, config.Value.QueueRoutingKey);

            _consumer = new EventingBasicConsumer(_channel);

            RegisterConsumers();
        }

        public void RegisterConsumers()
        {
            _consumer.Received += async (model, ea) =>
            {
                var gameString = Encoding.Default.GetString(ea.Body);
                var gameInfo = JsonConvert.DeserializeObject<GameInfo>(gameString);

                await _commandBus.SubmitAsync(new RegisterGameCommand(gameInfo));
            };

            _channel.BasicConsume(_gameRegisteredQueue, true, _consumer);
        }

        public void Publish(string queueName, string data)
        {
            var body = Encoding.Default.GetBytes(data);
            _channel.BasicPublish(exchange: "",
                                  routingKey: "",
                                  basicProperties: null,
                                  body: body);
        }
    }

}
