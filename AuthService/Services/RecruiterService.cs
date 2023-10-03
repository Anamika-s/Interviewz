using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AuthService.Model; 
using AuthService.MongoDBSettings;
using System.Security.Authentication;

namespace AuthService.Services
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IMongoCollection<Recruiter> _recruiters;

        public RecruiterService(IOptions<UserDatabaseSettings> settings)
        {
            string connectionString =
               @"mongodb://authserviceaccount:0IDEE5BVMQjOwuWdUXmf9RLgY0okH5pUqmSIdVHyxLBGuVakgKrA19HgSCt6WcTEvIVdKfAe6GC1ACDbRUcAfQ==@authserviceaccount.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@authserviceaccount@"; MongoClientSettings settings1 = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings1.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings1);


            //var client = new MongoClient(settings.Value.ConnectionString);
            var database = mongoClient.GetDatabase("UserDb");//.Value.DatabaseName);
            _recruiters = database.GetCollection<Recruiter>("Recruiter");
        }

        public async Task<IEnumerable<Recruiter>> GetRecruitersAsync()
        {
            return await _recruiters.Find(recruiter => true).ToListAsync();
        }

        public async Task<Recruiter> GetRecruiterByIdAsync(string id)
        {
            return await _recruiters.Find(recruiter => recruiter.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateRecruiterAsync(Recruiter recruiter)
        {
            // Check if a recruiter with the same email already exists
            var existingRecruiter = await _recruiters.Find(c => c.Email == recruiter.Email).FirstOrDefaultAsync();
            if (existingRecruiter != null)
            {
                // A recruiter with the same email already exists, return a conflict response
                throw new InvalidOperationException("A Recruiter with the same email already exists.");
            }
            //Hashing
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(recruiter.Password);
            recruiter.Password = hashedPassword;
            await _recruiters.InsertOneAsync(recruiter);
        }

        public async Task<bool> UpdateRecruiterAsync(string id, Recruiter recruiter)
        {
            var filter = Builders<Recruiter>.Filter.Eq(r => r.Id, id);
            var update = Builders<Recruiter>.Update
                .Set(r => r.Password, recruiter.Password)
                .Set(r => r.FirstName, recruiter.FirstName)
                .Set(r => r.LastName, recruiter.LastName)
                .Set(r => r.UserName, recruiter.UserName)
                .Set(r => r.Company, recruiter.Company)
                .Set(r => r.Email, recruiter.Email)
                .Set(r => r.PhoneNumber, recruiter.PhoneNumber);

            var updateResult = await _recruiters.UpdateOneAsync(filter, update);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteRecruiterAsync(string id)
        {
            var deleteResult = await _recruiters.DeleteOneAsync(recruiter => recruiter.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}