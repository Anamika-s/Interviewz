using System.Text;
using RabbitMQ.Client;

namespace InterviewService.Services
{
    public interface IProducerService
    {
        public bool PorduceMessage();
    }

    public class ProducerService : IProducerService
    {
        public bool PorduceMessage()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "email", durable: false, exclusive: false, autoDelete: false, arguments: null);

            string message = "candidateEamil#recruiterEmail";

            var body = Encoding.UTF8.GetBytes(message);
            try
            {
                channel.BasicPublish(exchange: string.Empty, routingKey: "email", basicProperties: null, body: body);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
    }
}
