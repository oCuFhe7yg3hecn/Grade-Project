﻿using GradeProject.GameRegService.Communication.Events;
using GradeProject.GameRegService.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Communication
{

    public enum Queues
    {
        gameRegService_testQueue
    }

    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _replyQueueName;
        private readonly EventingBasicConsumer _consumer;
        private readonly IBasicProperties _props;
        private readonly ILogger<RabbitMqEventBus> _logger;

        private readonly string _gameRegisteredQueue;

        public RabbitMqEventBus()
        {
            //_logger = logger;

            var connFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("GameRegisterExcchange", "direct");

            RegisterEventHandlers();

            //_props = _channel.CreateBasicProperties();
            //var corrId = Guid.NewGuid().ToString();
            //_props.ReplyTo = _replyQueueName;
            //_props.CorrelationId = corrId;

        }

        public void RegisterEventHandlers()
        {
            //_channel.BasicConsume(queueName, false, consumer);
        }

        public void AddToProfileService(GameInfo game)
        {
            var data = JsonConvert.SerializeObject(game);
            var body = Encoding.Default.GetBytes(data);

            _channel.BasicPublish(exchange: "GameRegisterExcchange",
                                  routingKey: "GameRegistered",
                                  basicProperties: null,
                                  body: body);
        }
    }


}
