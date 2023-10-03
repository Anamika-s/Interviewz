using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace InterviewService.Models
{
    public class TimeSlot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public int Duration { get; set; }
        public string MeetingLink { get; set; }
        public string RecruiterId { get; set; }
        public bool Booked { get; set; } = false;
    }
}
