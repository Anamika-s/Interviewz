using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AuthService.Model;
using AuthService.MongoDBSettings;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ICandidateService _candidateService;
        private readonly IRecruiterService _recruiterService;
            
        
        public AuthenticationController(ICandidateService candidateService, IRecruiterService recruiterService, IConfiguration config)
        {
            _candidateService = candidateService;
            _recruiterService = recruiterService;
            _config = config;
        }


        //string connectionString = 
        //@"mongodb://authserviceaccountauthservice:iL7q6uC4ZRLMqtTKKcstucAUXUxayKW5kwr1aJIefEDgSwanHioUqsp3i8TYb8YUQitSFq0d2fhFACDbNic3Xw==@authserviceaccountauthservice.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@authserviceaccountauthservice@";

        private readonly IConfiguration _config;
        private readonly IMongoCollection<Candidate> _candidatesCollection ;
        //private readonly IMongoCollection<Recruiter> _recruitersCollection; 
        //public AuthenticationController() {
        //        string CosmosDBAccountUri = "authserviceaccount.mongo.cosmos.azure.com";
        //       string CosmosDBAccountPrimaryKey = "0IDEE5BVMQjOwuWdUXmf9RLgY0okH5pUqmSIdVHyxLBGuVakgKrA19HgSCt6WcTEvIVdKfAe6GC1ACDbRUcAfQ==";

        //  string CosmosDbName = "UserDb";

        //  string CosmosDbContainer1Name = "Candidate";
        //    string CosmosDbContainer2Name = "Recruiter";

        //}






        //var databaseName = config["DatabaseName"];
        //var containerName = config["ContainerName"];
        //var account = config["Account"];
        //var key = config["Key"];
        //var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
        //var database =   client.CreateDatabaseIfNotExistsAsync(databaseName);
        //database.CreateContainerIfNotExistsAsync(containerName, "/id");
        //var cosmosDbService = new CosmosDbService(client, databaseName, containerName);

        //var client = new MongoClient(settings.ConnectionString);
        //var database = client.GetDatabase(settings.DatabaseName);
        //_candidatesCollection = database.GetCollection<Candidate>(settings.CandidatesCollectionName);
        //_recruitersCollection = database.GetCollection<Recruiter>(settings.RecruitersCollectionName);
        //_config = config;

        [HttpPost("candidate/login")]
        public IActionResult CandidateLogin([FromBody]  User candidate)
         {
            string candidateEmail = candidate.Email;
            string candidatePassword = candidate.Password;

            Candidate findCandidate = CheckCandidate(candidateEmail, candidatePassword);
            if (findCandidate != null)
            {
                var tokenString = GenerateToken(findCandidate.UserName);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        [HttpPost("recruiter/login")]
        public IActionResult RecruiterLogin([FromBody] User candidate)
        {
            string recruiterEmail = candidate.Email;
            string recruiterPassword = candidate.Password;
            Recruiter findRecruiter = CheckRecruiter(recruiterEmail, recruiterPassword);
            if (findRecruiter != null)
            {
                var tokenString = GenerateToken(findRecruiter.UserName);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        private string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, username),
        // Add more claims if needed
    };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(7), // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Candidate CheckCandidate(string candidateEmail, string candidatePassword)
        {
            string connectionString =
               @"mongodb://authserviceaccount:0IDEE5BVMQjOwuWdUXmf9RLgY0okH5pUqmSIdVHyxLBGuVakgKrA19HgSCt6WcTEvIVdKfAe6GC1ACDbRUcAfQ==@authserviceaccount.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@authserviceaccount@"; MongoClientSettings settings1 = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings1.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings1);


            mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("UserDb");//.Value.DatabaseName);

            var _candidatesCollection = database.GetCollection<Candidate>("Candidate"); Candidate candidate = _candidatesCollection.Find(u => u.Email == candidateEmail).SingleOrDefault();

            if (candidate != null && BCrypt.Net.BCrypt.Verify(candidatePassword, candidate.Password))
            {
                return candidate;
            }

            return null;
        }

        private Recruiter CheckRecruiter(string recruiterEmail, string recruiterPassword)
        {
            string connectionString =
           @"mongodb://authserviceaccount:0IDEE5BVMQjOwuWdUXmf9RLgY0okH5pUqmSIdVHyxLBGuVakgKrA19HgSCt6WcTEvIVdKfAe6GC1ACDbRUcAfQ==@authserviceaccount.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@authserviceaccount@"; MongoClientSettings settings1 = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
            settings1.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings1);


            mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("UserDb");//.Value.DatabaseName);
            var _recruitersCollection = database.GetCollection<Recruiter>("Recruiter"); 
            //Candidate candidate = _candidatesCollection.Find(u => u.Email == recruiterEmail).SingleOrDefault();

            Recruiter recruiter = _recruitersCollection.Find(u => u.Email == recruiterEmail).SingleOrDefault();

            if (recruiter != null && BCrypt.Net.BCrypt.Verify(recruiterPassword, recruiter.Password))
            {
                return recruiter;
            }

            return null;
        }
    }
}