using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailService.Service
{
    public interface IConsumerService
    {
        public string ConsumeMessage();
    }

    public class ConsumerService : IConsumerService
    {

        IEmailServe _emailServe;
        public ConsumerService(IEmailServe emailServe)
        {
            _emailServe = emailServe; 
        }

        public string ConsumeMessage()
        {
            var factory = new ConnectionFactory { HostName= "localhost"};

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "email", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            string message = "";
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Producer: {message}");
                _emailServe.SendEmail("", "shubhranshumohandas01062000@gmail.com");
            };

            channel.BasicConsume(queue: "email", autoAck: true, consumer: consumer);

            return message;
        }
    }
}
