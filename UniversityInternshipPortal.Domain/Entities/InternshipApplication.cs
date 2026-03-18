using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInternshipPortal.Domain.Entities
{
    public class InternshipApplication : BaseEntity
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int InternshipId { get; set; }

        public string Status { get; set; }

        public Student Student { get; set; }

        public Internship Internship { get; set; }
    }
}
