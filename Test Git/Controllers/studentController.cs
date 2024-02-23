using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NLog;
using System.Reflection;
using Test_Git.Models;
using Logger = NLog.Logger;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Test_Git.Controllers
{    
    public class studentController : Controller
    {
        public readonly ContextDb db;
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public studentController(ContextDb db)
        {
            this.db = db;
        }
        [Route("Add")]
        [HttpPost]
        public Students Inset([FromBody] Students students)
        {
            if (students == null)
                throw new Exception("data not found");
            db.stud.InsertOne(students);
            return students;
        }

        [HttpGet]
        [Route("Get")]
        public List<Students> GetStudents()
        {
            return db.stud.AsQueryable().ToList();
        }

        [Route("Update{id}")]
        [HttpPut]
        public bool updateStudent([FromBody] Students student, [FromRoute] string id)
        {
            
                try
                {
                    //session.StartTransaction();
                    //var filter = Builders<Students>.Filter.Eq(x => x._id, id);
                    var update = Builders<Students>.Update
                        .Set(x => x.StudentId, student.StudentId)
                        .Set(x => x.Description, student.Description)
                        .Set(x => x.Name, student.Name);
                    CommonUpdate(id, update);
                    
                }
                catch (Exception e)
                {                 
                    Logger.Error($"Error in {MethodBase.GetCurrentMethod().Name} {e.Message} {e.StackTrace}");
                    throw;
                }
                return true;
           
        }
        public void CommonUpdate(string id, UpdateDefinition<Students> update, IClientSessionHandle session = null)
        {
            if (session == null)
                db.stud.UpdateOne(id, update);
            else
                db.stud.UpdateOne(session: session, id, update);
        }
    }
}
