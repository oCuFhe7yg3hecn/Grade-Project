using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Communication
{
    public interface IEventBus
    {
        void Register(string queueName, EventingBasicConsumer consumer);
        void Publish(string queueName, string data);
    }

    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _replyQueueName;
        private readonly EventingBasicConsumer _consumer;
        private readonly IBasicProperties _props;

        public RabbitMqEventBus()
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

            var consumer = new EventingBasicConsumer(_channel);
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

        }
    }

    public enum Queues
    {
        gameRegService_testQueue
    }
}
