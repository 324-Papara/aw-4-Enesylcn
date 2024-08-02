using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Para.Bussiness.RabbitMQ
{
    public class RabbitMQPublisher : IDisposable
    {
        private readonly IConfiguration configurationg;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string _queueName;

        public RabbitMQPublisher(IConfiguration configuration)
        {
            var rabbitmqCfg = configurationg.GetSection("RabbitMQ");
            var factory = new ConnectionFactory()
            {
                HostName = rabbitmqCfg["HostName"],
                UserName = rabbitmqCfg["UserName"],
                Password = rabbitmqCfg["Password"]
            };


            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            _queueName = rabbitmqCfg["QueueName"]; ;
            channel.QueueDeclare(
                queue: _queueName,
                 durable: true,
                  exclusive: false,
                   autoDelete: false,
                    arguments: null);
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            channel?.Close();
            connection?.Close();
        }
    }
}