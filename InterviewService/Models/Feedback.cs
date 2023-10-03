using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace InterviewService.Models
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FeedbackID { get; set; }
        public int RecruiterID { get; set; }
        public int CandidateID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
