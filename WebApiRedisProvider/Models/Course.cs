using System;

namespace WebApiRedisProvider.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime BeginDate { get; set; }
        public int Credits { get; set; }
        public bool Finished { get; set; }

    }
}