using InterviewService.DAL;
using InterviewService.Models;

namespace InterviewService.Services
{
    public interface IBookingSlotService
    {
        public Task<BookingSlot> AddBookingslot(BookingSlot bookingSlot);
        public List<BookingSlot> GetAllBookingslotById(string id);
        public List<BookingSlot> GetAllBookingslot();
        //public bool UpdateBookingslot(BookingSlot bookingSlot);
        public bool DeleteBookingslot(string id);

    }

    public class BookingSlotService : IBookingSlotService
    {
        private IBookingSlotDAL _bookingslotDAL;

        public BookingSlotService(IBookingSlotDAL bookingslotDAL)
        {
            _bookingslotDAL = bookingslotDAL;
        }

        public async Task<BookingSlot> AddBookingslot(BookingSlot bookingSlot)
        {
            return await _bookingslotDAL.AddBookingslot(bookingSlot);
        }

        public bool DeleteBookingslot(string id)
        {
            return _bookingslotDAL.DeleteBookingslot(id);
        }

        public List<BookingSlot> GetAllBookingslotById(string id)
        {
            return _bookingslotDAL.GetAllBookingslotById(id);
        }

        public List<BookingSlot> GetAllBookingslot()
        {
            return _bookingslotDAL.GetAllBookingslot();
        }

        //public bool UpdateTimeslot(TimeSlot timeSlot)
        //{
        //    return _timeslotDAL.UpdateTimeslot(timeSlot);
        //}
    }
}
