namespace AuthService.MongoDBSettings
{
    public class UserDatabaseSettings : IUserDatabaseSettings
    {
        public string CandidatesCollectionName { get; set; }
        public string RecruitersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName{ get; set; }
    }
        public interface IUserDatabaseSettings
        {
            public string CandidatesCollectionName { get; set; }
            public string RecruitersCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }

        }
    }
