using AuthService.Model;

namespace AuthService
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Candidate>> GetMultipleAsync(string query);
        Task<Candidate> GetAsync(string id);
        Task AddAsync(Candidate item);
        Task UpdateAsync(string id, Candidate item);
        Task DeleteAsync(string id);
    }
}
