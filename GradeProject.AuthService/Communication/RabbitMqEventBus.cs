using GradeProject.AuthService.Models.Account.Register;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Communication
{
    public class RabbitMqBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;

        //Gueues
        private readonly string _gameRegisteredQueue;      

        public RabbitMqBus()
        {
            var connFactory = new ConnectionFactory() { HostName = RabbitMqConfig.HostName };
            _connection = connFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(RabbitMqConfig.RegisterProfileExchange, "direct");
        }

        public void Publish(string exchange, string key, byte[] data)
        {
            _channel.BasicPublish(exchange: exchange,
                                  routingKey: key,
                                  basicProperties: null,
                                  body: data);
        }

        public void SendProfile(ProfileRegisterModel profile)
        {
            Publish(RabbitMqConfig.RegisterProfileExchange, "register", Utils.ObjectConverter.ObjectToBytes(profile));
        }
    }

}
