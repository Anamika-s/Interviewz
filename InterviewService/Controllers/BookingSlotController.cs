using InterviewService.Models;
using InterviewService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingSlotController : ControllerBase
    {
        private readonly IBookingSlotService _bookingslotService;
        private readonly IProducerService _producerService;

        public BookingSlotController(IBookingSlotService bookingSlotService, IProducerService producerService)
        {
            _bookingslotService = bookingSlotService;
            _producerService = producerService;
        }

        //GET: api/<BookingSlotController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<BookingSlot> bookingslots = _bookingslotService.GetAllBookingslot();
            return Ok(bookingslots);
        }

        // GET api/<BookingSlotController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            List<BookingSlot> bookingslotList = _bookingslotService.GetAllBookingslotById(id);
            return bookingslotList.Count > 0 ? Ok(bookingslotList) : BadRequest();
        }

        // POST api/<BookingSlotController>
        [HttpPost]
        public async Task<IActionResult> Post(BookingSlot bookingslot)
        {
            return await _bookingslotService.AddBookingslot(bookingslot) != null ? Ok(bookingslot) : BadRequest();
        }

        //[HttpPost("bookslot")]
        //public async Task<IActionResult> BookSlot()
        //{
        //    return _producerService.PorduceMessage() ? Ok() : BadRequest();
        //}

        // PUT api/<BookingSlotController>/5
        //[HttpPut]
        //public async Task<IActionResult> Put(BookingSlot bookingslot)
        //{
        //    return _bookingslotService.UpdateBookingslot(bookingslot) ? Ok() : BadRequest();
        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return _bookingslotService.DeleteBookingslot(id) ? Ok() : BadRequest();
        }
    }
}
