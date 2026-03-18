using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInternshipPortal.Domain.Entities;

namespace UniversityInternshipPortal.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByUserIdAsync(int userId);
    }
}
