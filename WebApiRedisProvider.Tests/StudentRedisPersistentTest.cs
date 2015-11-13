using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiRedisProvider.Repositories;
using WebApiRedisProvider.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System.Collections.Generic;
using System.Linq;

namespace WebApiRedisProvider.Tests
{
    [TestClass]
    public class StudentRedisPersistentTest
    {
        private IStudentRepository _repository;
        private Student _student;
        private readonly RedisClient redis;
        private IRedisTypedClient<Student> redisStudents;

        public StudentRedisPersistentTest()
        {
            redis = new RedisClient("localhost");
        }

        [TestInitialize]
        public void Initialize()
        {
            RedisSeedDatabase();

            _repository = new StudentRedisRepository();

            _student = new Student
            {
                Id = redisStudents.GetNextSequence(),
                Name = "Rogerio Sá de Lima",
                Age = 35,
                Birthday = new DateTime(1980, 10, 31),
                Actived = true
            };
        }

        [TestMethod]
        public void AddStudentWithRedis()
        {
            var newStudent = _repository.Add(_student);

            Assert.IsTrue(newStudent.Id > 0);
        }

        [TestMethod]
        public void GetStudentWithRedis()
        {
            var student = _repository.Get(1);

            Assert.IsNotNull(student);
        }

        [TestMethod]
        public void DeleteStudentWithRedis()
        {
            _repository.Delete(1);

            var student = redisStudents.GetById(1);

            Assert.IsNull(student);
        }

        [TestMethod]
        public void UpdateStudentWithRedis()
        {
            var student = redisStudents.GetById(1);

            student.Actived = false;

            _repository.Update(student);

            var updStudent = redisStudents.GetById(1);

            Assert.AreEqual(updStudent.Actived, false);
        }

        [TestMethod]
        public void GetAllStudentWithRedis()
        {
            redisStudents.Store(_student);            

            IEnumerable<Student> students = _repository.GetAll();

            Assert.IsTrue(students.Count() == 2);
        }

        private void RedisSeedDatabase()
        {
            redis.FlushAll();

            redisStudents = redis.As<Student>();

            var persistedStudent = new Student
            {
                Id = redisStudents.GetNextSequence(),
                Name = "Hugo Estevam Longo",
                Age = 30,
                Birthday = new DateTime(1985, 10, 31),
                Actived = true
            };

            redisStudents.Store(persistedStudent);

        }
    }
}
