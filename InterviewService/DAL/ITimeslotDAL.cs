using InterviewService.Models;
using InterviewService.MongoDbSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace InterviewService.DAL
{
    public interface ITimeslotDAL
    {
        Task<TimeSlot> AddTimeslot(TimeSlot timeSlot);
        List<TimeSlot> GetAllTimeslot();
        Task<TimeSlot> GetTimeslotById(string id);
        Task<bool> UpdateTimeslot(string id, bool isBooked);
        bool DeleteTimeslot(string id);
    }

    public class TimeslotDAL : ITimeslotDAL
    {
        private readonly IMongoCollection<TimeSlot> _timeslots;

        public TimeslotDAL(IOptions<InterviewDBSettings> settings)
        {
            //var client = new MongoClient(settings.Value.ConnectionString);
            //var database = client.GetDatabase(settings.Value.DatabaseName);
            //_timeslots = database.GetCollection<TimeSlot>(settings.Value.TimeSlotCollectionName);
            string connectionString =
    @"mongodb://interviewaccount:KpnsJlvBv78Ibq1mMNWd0niUQN82afATFfozvnTksOLuIfEssstFdxOg7TZc66bZXVGuGSL8vVa0ACDb0ndgJg==@interviewaccount.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@interviewaccount@";
            MongoClientSettings settings1 = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings1.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings1);


            mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("InterviewDb");//.Value.DatabaseName);
            _timeslots = database.GetCollection<TimeSlot>("TimeSlot");
        }

        public async Task<TimeSlot> AddTimeslot(TimeSlot timeSlot)
        {
            try
            {
                if (await CheckForOverlap(timeSlot))
                {
                    await _timeslots.InsertOneAsync(timeSlot);
                    return timeSlot; // The ID will be updated in the timeSlot object
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private async Task<bool> CheckForOverlap(TimeSlot newTimeSlot)
        {
            var currentTime = DateTime.Now;

            // Convert the date and start time strings to DateTime objects
            DateTime newStartTime = DateTime.ParseExact(newTimeSlot.StartTime, "hh:mm tt", null);
            DateTime newEndTime = newStartTime.AddMinutes(newTimeSlot.Duration);

            // Fetch existing time slots from MongoDB
            var existingTimeSlots = await _timeslots.Find(ts => ts.Date == newTimeSlot.Date).ToListAsync();

            foreach (var existingTimeSlot in existingTimeSlots)
            {
                DateTime existingStartTime = DateTime.ParseExact(existingTimeSlot.StartTime, "hh:mm tt", null);
                DateTime existingEndTime = existingStartTime.AddMinutes(existingTimeSlot.Duration);

                // Check for overlap with the existing time slot
                if (newStartTime < existingEndTime && existingStartTime < newEndTime)
                {
                    return false; // Overlap detected
                }
            }

            return true; // No overlap detected
        }

        public bool DeleteTimeslot(string id)
        {
            _timeslots.DeleteOneAsync(x => x.Id.ToString() == id);
            return true;
        }

        public async Task<TimeSlot> GetTimeslotById(string id)
        {
            try
            {
                var filter = Builders<TimeSlot>.Filter.Eq(x => x.Id, id);
                TimeSlot timeslot = await _timeslots.Find(filter).FirstOrDefaultAsync();
                return timeslot;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<TimeSlot> GetAllTimeslot()
        {
            List<TimeSlot> timeslotList = _timeslots.FindAsync(x => true).Result.ToList();
            return timeslotList;
        }

        public async Task<bool> UpdateTimeslot(string id, bool isBooked)
        {
            try
            {
                var filter = Builders<TimeSlot>.Filter.Eq(x => x.Id, id);
                var update = Builders<TimeSlot>.Update.Set(x => x.Booked, isBooked);

                var result = await _timeslots.UpdateOneAsync(filter, update);

                if (result.ModifiedCount > 0)
                {
                    return true; // Update successful
                }

                return false; // Timeslot with the specified Id not found
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false; // Update failed due to an error
            }
        }
    }
}
