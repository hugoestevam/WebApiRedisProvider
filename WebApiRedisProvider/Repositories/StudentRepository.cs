using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRedisProvider.Infrastructures;
using WebApiRedisProvider.Models;

namespace WebApiRedisProvider.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private StudentDbContext db;
        public StudentRepository()
        {
            db = new StudentDbContext();
        }

        public Student Add(Student student)
        {
            var std = db.Students.Add(student);
            db.SaveChanges();
            return std;
        }

        public void Delete(int id)
        {
            var student = db.Students.Find(id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }

        }

        public Student Get(int id)
        {            
            return db.Students.Find(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students.ToList();
        }

        public Student Update(Student student)
        {
            var entry = db.Entry(student);
            entry.State = EntityState.Modified;
            db.SaveChanges();          
            return entry.Entity;
        }
    }
}
