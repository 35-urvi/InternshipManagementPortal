using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInternshipPortal.Application.DTOs
{
    public class ApplyInternshipRequest
    {
        public int StudentId { get; set; }

        public int InternshipId { get; set; }
    }
}
