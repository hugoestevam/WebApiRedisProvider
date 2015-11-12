using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiRedisProvider.Infrastructures;
using WebApiRedisProvider.Models;
using WebApiRedisProvider.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace WebApiRedisProvider.Tests
{
    [TestClass]
    public class StudentEFPersistentTest
    {
        private IStudentRepository _repository;
        private Student _student;
        private StudentDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            Database.SetInitializer(new StudentBaseTest());
            
            _repository = new StudentRepository();

            _context = new StudentDbContext();

            _student = new Student
            {
                Name = "Rogerio Sá de Lima",
                Age = 35,
                Birthday = new DateTime(1980, 10, 31),
                Actived = true
            };
        }

        [TestMethod]
        public void AddStudentWithEF()
        {
            var newStudent = _repository.Add(_student);

            Assert.IsTrue(newStudent.Id > 0);
        }

        [TestMethod]
        public void GetStudentWithEF()
        {
            var student = _repository.Get(1);

            Assert.IsNotNull(student);
        }

        [TestMethod]
        public void DeleteStudentWithEF()
        {
            _repository.Delete(1);

            var student = _repository.Get(1);

            Assert.IsNull(student);
        }

        [TestMethod]
        public void UpdateStudentWithEF()
        {            
            var student = _context.Database.SqlQuery<Student>("Select * From Students where Id = 1").FirstOrDefault();

            student.Actived = false;

            _repository.Update(student);

            var updStudent = _context.Database.SqlQuery<Student>("Select * From Students where Id = 1").FirstOrDefault();
            
            Assert.AreEqual(updStudent.Actived, false);
        }

        [TestMethod]
        public void GetAllStudentWithEF()
        {
            _context.Students.Add(_student);
            _context.SaveChanges();

            IEnumerable<Student> students = _repository.GetAll();

            Assert.IsTrue(students.Count() == 2);
        }
    }
}
