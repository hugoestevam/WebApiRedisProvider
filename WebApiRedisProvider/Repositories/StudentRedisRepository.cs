using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRedisProvider.Models;

namespace WebApiRedisProvider.Repositories
{
    public class StudentRedisRepository : IStudentRepository, IDisposable
    {
        private readonly RedisClient redis;
        private IRedisTypedClient<Student> redisStudents;

        public StudentRedisRepository()
        {
            redis = new RedisClient("ndd.redis.cache.windows.net", 6379, "W7eXZLH8cqlhaOJeTwJgsYVtenbqnibQKFZN3wdaUk0=");
            redisStudents = redis.As<Student>();
        }


        public Student Add(Student student)
        {
            return redisStudents.Store(student);
        }

        public void Delete(int id)
        {
            redisStudents.DeleteById(id);
        }

        public Student Get(int id)
        {
            return redisStudents.GetById(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return redisStudents.GetAll();
        }

        public Student Update(Student student)
        {
            return redisStudents.Store(student);
        }
        
        public void Dispose()
        {
            redis.Dispose();            
        }
    }
}
