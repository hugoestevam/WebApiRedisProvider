using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiRedisProvider.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace WebApiRedisProvider.Controllers
{
    public class StudentsController : ApiController
    {
        readonly RedisClient redis = new RedisClient("localhost");
        private IRedisTypedClient<Student> redisStudents;

        public StudentsController()
        {
            redisStudents = redis.As<Student>();
        }

        // GET: api/Students
        public IEnumerable<Student> Get()
        {
            return redisStudents.GetAll();            
        }

        // GET: api/Students/5
        public Student Get(int id)
        {            
            return redisStudents.GetById(id);
        }

        // POST: api/Students
        public void Post([FromBody]Student value)
        {
            redisStudents.Store(value);
        }

        // PUT: api/Students/5
        public void Put(int id, [FromBody]Student value)
        {
            var student = redisStudents.GetById(value.Id);
            redisStudents.Store(value);
        }

        // DELETE: api/Students/5
        public void Delete(int id)
        {
            redisStudents.DeleteById(id);
        }
    }
}
