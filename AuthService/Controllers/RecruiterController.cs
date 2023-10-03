using Microsoft.AspNetCore.Mvc;
using AuthService.Model;
using AuthService.Services;

namespace AuthService.Controllers
{
    [Route("api/recruiters")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;

        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recruiter>>> GetRecruiters()
        {
            var recruiters = await _recruiterService.GetRecruitersAsync();
            return Ok(recruiters);
        }

        [HttpGet("{id:length(24)}", Name = "GetRecruiter")]
        public async Task<ActionResult<Recruiter>> GetRecruiter(string id)
        {
            var recruiter = await _recruiterService.GetRecruiterByIdAsync(id);
            if (recruiter == null)
            {
                return NotFound();
            }
            return Ok(recruiter);
        }

        [HttpPost]
        public async Task<ActionResult<Recruiter>> CreateRecruiter(Recruiter recruiter)
        {
            await _recruiterService.CreateRecruiterAsync(recruiter);
            return CreatedAtRoute("GetRecruiter", new { id = recruiter.Id }, recruiter);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateRecruiter(string id, Recruiter recruiter)
        {
            var updated = await _recruiterService.UpdateRecruiterAsync(id, recruiter);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteRecruiter(string id)
        {
            var deleted = await _recruiterService.DeleteRecruiterAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
