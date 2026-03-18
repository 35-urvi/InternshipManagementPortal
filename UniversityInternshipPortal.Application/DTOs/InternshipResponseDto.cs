using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInternshipPortal.Application.DTOs
{
    public class InternshipResponseDto
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RequiredSkills { get; set; }

        public decimal Stipend { get; set; }

        public string CompanyName { get; set; }
    }
}
