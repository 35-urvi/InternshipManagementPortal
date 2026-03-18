using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInternshipPortal.Domain.Entities
{
    public class Student : BaseEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public double CGPA { get; set; }

        public User User { get; set; }
    }
}
