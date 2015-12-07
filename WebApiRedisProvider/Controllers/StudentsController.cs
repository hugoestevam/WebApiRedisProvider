using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiRedisProvider.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using WebApiRedisProvider.Repositories;

namespace WebApiRedisProvider.Controllers
{
    public class StudentsController : ApiController
    {
        private IStudentRepository repository; 

        public StudentsController()
        {
            repository = new StudentRedisRepository();
             
        }

        // GET: api/Students
        public IEnumerable<Student> Get()
        {
            return repository.GetAll();            
        }

        // GET: api/Students/5
        public Student Get(int id)
        {            
            return repository.Get(id);
        }

        // POST: api/Students
        public void Post([FromBody]Student value)
        {
            repository.Add(value);
        }

        // PUT: api/Students/5
        public void Put(int id, [FromBody]Student value)
        {
            var student = repository.Get(id);
            if (student != null)
                repository.Update(value);
        }

        // DELETE: api/Students/5
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
