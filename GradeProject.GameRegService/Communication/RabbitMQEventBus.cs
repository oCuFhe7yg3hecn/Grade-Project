using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeProject.GameRegService.Communication.Events;
using GradeProject.GameRegService.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace GradeProject.GameRegService.Communication
{
    //public class RabbitMQEventBus : IEventBus, IDisposable
    //{
    //    private readonly string _hostName;
    //    private readonly string _exchangeName;

    //    public RabbitMQEventBus(IOptions<RabbitBusOptions> options)
    //    {
    //        _hostName = options.Value.HostName;
    //        _exchangeName = options.Value.ExchangeName;
    //    }

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Publish(IntegrationEvent @event)
    //    {
    //        var eventName = @event.GetType().Name;
    //        var factory = new ConnectionFactory() { HostName = _hostName };

    //        using (var connection = factory.CreateConnection())
    //        using (var channel = connection.CreateModel())
    //        {
    //            channel.ExchangeDeclare(exchange: _exchangeName, type: "fanout");

    //            string message = JsonConvert.SerializeObject(@event);
    //            var body = Encoding.UTF8.GetBytes(message);

    //            channel.BasicPublish(exchange: _exchangeName, routingKey: eventName, basicProperties: null, body: body);
    //        }
    //    }

    //    public void Recieve()
    //    {
    //        var factory = new ConnectionFactory() { HostName = _hostName };

    //        using (var connection = factory.CreateConnection())
    //        using (var channel = connection.CreateModel())
    //        {

    //        }

        //public void Subscribe<T, TH>()
        //    where T : IntegrationEvent
        //    where TH : IIntegrationEventHandler<T>
        //{
        //    throw new NotImplementedException();
        //}

        //public void Unsubscribe<T, TH>()
        //    where T : IntegrationEvent
        //    where TH : IIntegrationEventHandler<T>
        //{
        //    throw new NotImplementedException();
        //}
    //}
}
