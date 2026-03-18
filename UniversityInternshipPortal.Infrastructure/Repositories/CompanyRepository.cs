using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInternshipPortal.Application.Interfaces;
using UniversityInternshipPortal.Domain.Entities;
using UniversityInternshipPortal.Infrastructure.Data;

namespace UniversityInternshipPortal.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Company?> GetByUserIdAsync(int userId)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
