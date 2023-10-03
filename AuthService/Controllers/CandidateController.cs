using Microsoft.AspNetCore.Mvc;
using AuthService.Model;
using AuthService.Services;
using MongoDB.Driver;
using System.Security.Authentication;

namespace AuthService.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            var candidates = await _candidateService.GetCandidatesAsync();
            return Ok(candidates);
        }

        [HttpGet("{id:length(24)}", Name = "GetCandidate")]
        public async Task<ActionResult<Candidate>> GetCandidate(string id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<ActionResult<Candidate>> CreateCandidate(Candidate candidate)
        {
            await _candidateService.CreateCandidateAsync(candidate);
            return CreatedAtRoute("GetCandidate", new { id = candidate.CandidateId }, candidate);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCandidate(string id, Candidate candidate)
        {
            var updated = await _candidateService.UpdateCandidateAsync(id, candidate);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCandidate(string id)
        {
            var deleted = await _candidateService.DeleteCandidateAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
