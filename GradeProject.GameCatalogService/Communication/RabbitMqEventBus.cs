using GradeProject.GameCatalogService.Communication.Events;
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
    public enum Queues
    {
        gameRegService_testQueue
    }

    public class RabbitMqBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        //private readonly string _replyQueueName;
        //private readonly EventingBasicConsumer _consumer;
        //private readonly IBasicProperties _props;

        //Gueues
        private readonly string _gameRegisteredQueue;

        //Dependencies
        private readonly IGamesService _gameSvc;

        public RabbitMqBus(IOptions<RabbitMqConfig> config, IGamesService gamesSvc)
        {
            _gameSvc = gamesSvc;

            var connFactory = new ConnectionFactory() { HostName = config.Value.HostName };
            _connection = connFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(config.Value.Exchange, "direct");

            _gameRegisteredQueue = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(_gameRegisteredQueue, config.Value.Exchange, config.Value.QueueRoutingKey);

            RegisterConsumers();

        }

        public void RegisterConsumers()
        {
            //EventHandlers

            //var gameRecieved = new GameRegisteredEventHandler();

            //Game Regitered
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += gameRecieved.Consumer_Received;
            //consumer.Received += async (model, ea) =>
            //{
            //    var responseString = Encoding.Default.GetString(ea.Body);
            //    var gameInfo = JsonConvert.DeserializeObject<GameInfo>(responseString);
            //    await gameRecieved.Handle(gameInfo);
            //    //_gameSvc.AddGameAsync(gameInfo);
            //};
            _channel.BasicConsume(_gameRegisteredQueue, true, consumer);

            //Some Another
        }

        public void Register(string queueName, EventingBasicConsumer consumer)
        {
            consumer.Model = _channel;
            _channel.BasicConsume(queueName, false, consumer);
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
