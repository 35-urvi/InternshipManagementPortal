using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInternshipPortal.Domain.Entities
{
    public class Internship : BaseEntity
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RequiredSkills { get; set; }

        public double Stipend { get; set; }

        public Company? Company { get; set; }
    }
}
