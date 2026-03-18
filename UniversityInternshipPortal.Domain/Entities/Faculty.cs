using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInternshipPortal.Domain.Entities
{
    public class Faculty:BaseEntity
    {
        public int UserId { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        public User User { get; set; }
    }
}
