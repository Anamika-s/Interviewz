using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AuthService.Model;
using AuthService.MongoDBSettings;
using System.Security.Authentication;

namespace AuthService.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IMongoCollection<Candidate> _candidates;

        public CandidateService(IOptions<UserDatabaseSettings> settings)
        {
            string connectionString =
   @"mongodb://interviewaccount:KpnsJlvBv78Ibq1mMNWd0niUQN82afATFfozvnTksOLuIfEssstFdxOg7TZc66bZXVGuGSL8vVa0ACDb0ndgJg==@interviewaccount.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@interviewaccount@";
            MongoClientSettings settings1 = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings1.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings1);
            var database = mongoClient.GetDatabase("UserDb");//.Value.DatabaseName);
            _candidates = database.GetCollection<Candidate>("Candidate");
        }

        public async Task<IEnumerable<Candidate>> GetCandidatesAsync()
        {
            return await _candidates.Find(candidate => true).ToListAsync();
        }

        public async Task<Candidate> GetCandidateByIdAsync(string id)
        {
            return await _candidates.Find(candidate => candidate.CandidateId == id).FirstOrDefaultAsync();
        }

        public async Task CreateCandidateAsync(Candidate candidate)
        {
            // Check if a candidate with the same email already exists
            var existingCandidate = await _candidates.Find(c => c.Email == candidate.Email).FirstOrDefaultAsync();
            if (existingCandidate != null)
            {
                // A candidate with the same email already exists, return a conflict response
                throw new InvalidOperationException("A candidate with the same email already exists.");
            }
            //Hashing
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(candidate.Password);
            candidate.Password= hashedPassword;
            await _candidates.InsertOneAsync(candidate);
        }

        public async Task<bool> UpdateCandidateAsync(string id, Candidate candidate)
        {
            var filter = Builders<Candidate>.Filter.Eq(c => c.CandidateId, id);
            var update = Builders<Candidate>.Update
                .Set(c => c.UserName, candidate.UserName)
                .Set(c => c.Password, candidate.Password)
                .Set(c => c.FirstName, candidate.FirstName)
                .Set(c => c.LastName, candidate.LastName)
                .Set(c => c.Email, candidate.Email)
                .Set(c => c.PhoneNumber, candidate.PhoneNumber)
                .Set(c => c.Skills, candidate.Skills)
                .Set(c => c.Experience, candidate.Experience);

            var updateResult = await _candidates.UpdateOneAsync(filter, update);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCandidateAsync(string id)
        {
            var deleteResult = await _candidates.DeleteOneAsync(candidate => candidate.CandidateId == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
