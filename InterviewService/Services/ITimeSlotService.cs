using InterviewService.DAL;
using InterviewService.Models;

namespace InterviewService.Services
{
    public interface ITimeSlotService
    {
        public Task<TimeSlot> AddTimeslot(TimeSlot timeSlot);
        public Task<TimeSlot> GetTimeslotById(string id);
        public List<TimeSlot> GetAllTimeslot();
        public Task<bool> UpdateTimeslot(string id, bool isBooked);
        public bool DeleteTimeslot(string id);

    }

    public class TimeSlotService : ITimeSlotService
    {
        private ITimeslotDAL _timeslotDAL;

        public TimeSlotService(ITimeslotDAL timeslotDAL)
        {
            _timeslotDAL = timeslotDAL;
        }

        public async Task<TimeSlot> AddTimeslot(TimeSlot timeSlot)
        {
            return await _timeslotDAL.AddTimeslot(timeSlot);
        }

        public bool DeleteTimeslot(string id)
        {
            return _timeslotDAL.DeleteTimeslot(id);
        }

        public async Task<TimeSlot> GetTimeslotById(string id)
        {
            return await _timeslotDAL.GetTimeslotById(id);
        }

        public List<TimeSlot> GetAllTimeslot()
        {
            return _timeslotDAL.GetAllTimeslot();
        }

        public async Task<bool> UpdateTimeslot(string id, bool isBooked)
        {
            return await _timeslotDAL.UpdateTimeslot(id, isBooked);
        }
    }
}
