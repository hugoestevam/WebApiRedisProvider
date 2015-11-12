using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRedisProvider.Models;

namespace WebApiRedisProvider.Infrastructures
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext()
            : base("Name=StudentDbContext")
        {
            
        }

        public DbSet<Student> Students { get; set; }

        
    }
}
