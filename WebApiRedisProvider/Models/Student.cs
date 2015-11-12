using System;
using System.Collections.Generic;

namespace WebApiRedisProvider.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public bool Actived { get; set; }
        public List<Course> Courses { get; set; }
    }
}
