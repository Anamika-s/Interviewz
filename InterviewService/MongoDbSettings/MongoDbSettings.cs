using InterviewService.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace InterviewService.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<TimeSlot> TimeSlots => _database.GetCollection<TimeSlot>("TimeSlots");
       // public IMongoCollection<Feedback> Feedbacks => _database.GetCollection<Feedback>("Feedbacks");
        public IMongoCollection<BookingSlot> BookingSlots => _database.GetCollection<BookingSlot>("BookingSlots");
    }
}
