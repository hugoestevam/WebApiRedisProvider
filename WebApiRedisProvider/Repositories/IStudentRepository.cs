using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRedisProvider.Models;

namespace WebApiRedisProvider.Repositories
{
    public interface IStudentRepository
    {
        Student Add(Student student);
        Student Update(Student student);
        Student Get(int id);
        void Delete(int id);
        IEnumerable<Student> GetAll();
    }
}
