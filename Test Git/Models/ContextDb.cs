using MongoDB.Driver;

namespace Test_Git.Models
{
    public class ContextDb
    {
        public readonly IMongoDatabase db;

        public ContextDb(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("mongodb"));
            db = client.GetDatabase(configuration["ConnectionStrings:DatabaseName"]);
            
            stud = db.GetCollection<Students>("student1");
        }
        public readonly IMongoCollection<Students> stud;
        public readonly MongoClient client;
    }
}
