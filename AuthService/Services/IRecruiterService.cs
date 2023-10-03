using AuthService.Model;

namespace AuthService.Services
{
    public interface IRecruiterService
    {
        Task<IEnumerable<Recruiter>> GetRecruitersAsync();
        Task<Recruiter> GetRecruiterByIdAsync(string id);
        Task CreateRecruiterAsync(Recruiter recruiter);
        Task<bool> UpdateRecruiterAsync(string id, Recruiter recruiter);
        Task<bool> DeleteRecruiterAsync(string id);
    }
}
