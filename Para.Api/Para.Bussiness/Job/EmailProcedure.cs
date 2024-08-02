using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Para.Bussiness.Notification;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Para.Bussiness.Job
{
    public class EmailProcedure
    {
        private readonly IConfiguration configuration;
        private readonly INotificationService notificationService;

        public EmailProcedure(INotificationService notificationService, IConfiguration configuration)
        {
            this.notificationService = notificationService;
            this.configuration = configuration;
        }

        public void ProcessEmailQueue()
        {
            var rabbitmqCfg = this.configuration.GetSection("RabbitMQ");
            var factory = new ConnectionFactory()
            {
                HostName = rabbitmqCfg["HostName"],
                UserName = rabbitmqCfg["UserName"],
                Password = rabbitmqCfg["Password"]
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var email = JsonConvert.DeserializeObject<dynamic>(message);

                    string subject = Convert.ToString(email.Subject);
                    string emailTo = Convert.ToString(email.Email);
                    string content = Convert.ToString(email.Content);

                    notificationService.SendEmail(subject, emailTo, content);
                };

                channel.BasicConsume(queue: "emailQueue", autoAck: true, consumer: consumer);
            }
        }

    }
}