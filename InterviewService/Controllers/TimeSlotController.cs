using InterviewService.Models;
using InterviewService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterviewService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotService _timeslotService;

        public TimeSlotController(ITimeSlotService timeSlotService)
        {
            _timeslotService = timeSlotService;
        }

        //GET: api/<TimeSlotController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<TimeSlot> timeslots = _timeslotService.GetAllTimeslot();
            return Ok(timeslots);
        }

        // GET api/<TimeSlotController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            TimeSlot timeslot = await _timeslotService.GetTimeslotById(id);
            return timeslot != null ? Ok(timeslot) : BadRequest();
        }

        // POST api/<TimeSlotController>
        [HttpPost]
        public async Task<IActionResult> Post(TimeSlot timeslot)
        {
            TimeSlot createdTimeslot = await _timeslotService.AddTimeslot(timeslot);
            return  createdTimeslot != null ? Ok(createdTimeslot) : BadRequest();
        }

        // PUT api/<TimeSlotController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromQuery] bool isBooked)
        {
            Console.WriteLine($"id: {id} timeslot booked statues: {isBooked}");
            return await _timeslotService.UpdateTimeslot(id,isBooked) ? Ok($"Timeslot Updated: {isBooked}") : BadRequest();
        }

        // DELETE api/<TimeSlotController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return _timeslotService.DeleteTimeslot(id) ? Ok() : BadRequest();
        }
    }
}
