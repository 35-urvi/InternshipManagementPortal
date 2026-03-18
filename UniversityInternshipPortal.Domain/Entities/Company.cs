using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInternshipPortal.Domain.Entities
{
    public class Company:BaseEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string CompanyName { get; set; }

        public string Industry { get; set; }

        public User User { get; set; }
    }
}
