using GradeProject.GameRegService.Communication.Events;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Communication
{
    public interface IEventBus
    {
        void Register(string queueName, EventingBasicConsumer consumer);
        void Publish(string queueName, string data);
    }

    public class RabbitMqBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _replyQueueName;
        private readonly EventingBasicConsumer _consumer;
        private readonly IBasicProperties _props;
        private readonly ILogger<RabbitMqBus> _logger;

        public RabbitMqBus()
        {
            //_logger = logger;

            var connFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "gameRegService_testQueue",
                                  durable: false, 
                                  exclusive: false, 
                                  autoDelete: false, 
                                  arguments: null);

            var consumer = new TestEventConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var res = Encoding.Default.GetString(ea.Body);
                throw new Exception(res);
            };

            _channel.BasicConsume("gameRegService_testQueue", true, consumer);

            _props = _channel.CreateBasicProperties();
            var corrId = Guid.NewGuid().ToString();
            _props.ReplyTo = _replyQueueName;
            _props.CorrelationId = corrId;

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

            _logger.LogInformation($"Event with data : {data} was sent to {queueName}");
        }
    }

    public enum Queues
    {
        gameRegService_testQueue
    }
}
