using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRedisProvider.Models;
using WebApiRedisProvider.Repositories;

namespace WebApiRedisProvider.Controllers
{
    public class StudentsEFController : ApiController
    {
        private IStudentRepository repository;

        public StudentsEFController()
        {
            repository = new StudentRepository();
        }

        // GET: api/StudentsEF
        public IEnumerable<Student> Get()
        {
            return repository.GetAll();
        }

        // GET: api/StudentsEF/5
        public Student Get(int id)
        {
            return repository.Get(id);
        }

        // POST: api/StudentsEF
        public void Post([FromBody]Student value)
        {
            repository.Add(value);
        }

        // PUT: api/StudentsEF/5
        public void Put(int id, [FromBody]Student value)
        {
            repository.Update(value);
        }

        // DELETE: api/StudentsEF/5
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
