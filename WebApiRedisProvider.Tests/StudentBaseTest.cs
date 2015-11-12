using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRedisProvider.Infrastructures;
using WebApiRedisProvider.Models;

namespace WebApiRedisProvider.Tests
{
    public class StudentBaseTest : DropCreateDatabaseAlways<StudentDbContext>
    {
        private Student _student;

        public StudentBaseTest()
        {
            _student = new Student
            {
                Name = "Hugo Estevam Longo",
                Age = 30,
                Birthday = new DateTime(1985, 10, 31),
                Actived = true
            };
        }

        public override void InitializeDatabase(StudentDbContext context)
        {                        
            base.InitializeDatabase(context);
        }

        protected override void Seed(StudentDbContext context)
        {
            context.Students.Add(_student);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
