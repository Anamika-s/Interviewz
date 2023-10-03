using AuthService.Model;

namespace AuthService.Services
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetCandidatesAsync();
        Task  <Candidate> GetCandidateByIdAsync(string id);
        Task CreateCandidateAsync(Candidate candidate);
        Task<bool> UpdateCandidateAsync(string id, Candidate candidate);
        Task<bool> DeleteCandidateAsync(string id);
    }
}
