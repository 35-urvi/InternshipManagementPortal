using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInternshipPortal.Domain.Entities;

namespace UniversityInternshipPortal.Application.Interfaces
{
    public interface IInternshipRepository : IGenericRepository<Internship>
    {
        Task<IEnumerable<Internship>> GetInternshipsByCompany(int companyId);
        Task<IEnumerable<Internship>> GetAllAsync();
        Task<Internship?> GetByIdAsync(int id);
        Task AddAsync(Internship internship);
        Task DeleteAsync(Internship internship);
        Task SaveAsync();

    }
}
