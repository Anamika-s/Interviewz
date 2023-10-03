
﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace InterviewService.Models
{
    public class BookingSlot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? BookingID { get; set; }
        public string? RecruiterEmail { get; set; } 
        public string? MeetingLink { get; set; }
        public string? CandidateEmail { get; set; }
        public string? TimeSlotID { get; set; }
        public string Status { get; set; } = "upcoming";
    }
}
