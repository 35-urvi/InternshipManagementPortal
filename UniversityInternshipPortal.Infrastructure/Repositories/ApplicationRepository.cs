using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInternshipPortal.Domain.Entities;
using UniversityInternshipPortal.Infrastructure.Data;
using UniversityInternshipPortal.Application.Interfaces;

namespace UniversityInternshipPortal.Infrastructure.Repositories
{
    public class ApplicationRepository : GenericRepository<InternshipApplication>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<InternshipApplication>> GetApplicationsByStudent(int studentId)
        {
            return await _context.Applications
                .Where(a => a.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<InternshipApplication>> GetApplicationsByInternship(int internshipId)
        {
            return await _context.Applications
                .Where(a => a.InternshipId == internshipId)
                .ToListAsync();
        }
    }
}
