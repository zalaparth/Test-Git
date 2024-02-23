using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test_Git.Models
{
    public class Students
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int StudentId { get; set; }       
    }
}
