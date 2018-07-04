using GradeProject.GameRegService.Config;
using GradeProject.GameRegService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private RabbitMqConfig _config;

        private readonly ILogger<RabbitMqEventBus> _logger;

        public RabbitMqEventBus(IOptions<RabbitMqConfig> config)
        {
            _config = config.Value;

            var connFactory = new ConnectionFactory() { HostName = _config.HostName };
            _connection = connFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_config.Exchange, "direct");
        }

        public void Publish(byte[] body)
        {
            _channel.BasicPublish(exchange: _config.Exchange,
                                  routingKey: _config.QueueRoutingKey,
                                  basicProperties: null,
                                  body: body);
        }
    }


}
