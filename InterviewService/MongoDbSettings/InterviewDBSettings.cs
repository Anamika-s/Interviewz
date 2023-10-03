//using InterviewService.Models;
//using Microsoft.Extensions.Configuration;
//using MongoDB.Driver;

//namespace InterviewService.Settings
//{
//    public class InterviewDBSettings
//    {
//        public string ConnectionString { get; set; }
//        public string DatabaseName { get; set; }
//    }

//    public class MongoDbContext
//{
//    private readonly IMongoDatabase _database;

//    public MongoDbContext(IConfiguration configuration)
//    {
//        var settings = configuration.GetSection(nameof(InterviewDBSettings)).Get<InterviewDBSettings>();

//        var client = new MongoClient(settings.ConnectionString);
//        _database = client.GetDatabase(settings.DatabaseName);
//    }

//    public IMongoCollection<TimeSlot> TimeSlots => _database.GetCollection<TimeSlot>("TimeSlots");
//    public IMongoCollection<Feedback> Feedbacks => _database.GetCollection<Feedback>("Feedbacks");
//    public IMongoCollection<BookingSlot> BookingSlots => _database.GetCollection<BookingSlot>("BookingSlots");
//}
//}



namespace InterviewService.MongoDbSettings
{
    public class InterviewDBSettings: IInterviewDBSettings
    {
        public string BookingSlotCollectionName { get; set; }
        public string TimeSlotCollectionName { get; set; }
        public string FeedbackCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IInterviewDBSettings
    {
        public string BookingSlotCollectionName { get; set; }
        public string TimeSlotCollectionName { get; set; }
        public string FeedbackCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}
