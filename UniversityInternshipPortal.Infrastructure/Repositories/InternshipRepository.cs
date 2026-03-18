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
    public class InternshipRepository : GenericRepository<Internship>, IInternshipRepository
    {
        public InternshipRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Internship>> GetInternshipsByCompany(int companyId)
        {
            return await _context.Internships
                .Where(i => i.CompanyId == companyId)
                .ToListAsync();
        }

        //public async Task<IEnumerable<Internship>> GetAllAsync()
        //{
        //    return await _context.Internships.ToListAsync();
        //}
        public async Task<List<Internship>> GetAllAsync()
        {
            return await _context.Internships
                .Include(i => i.Company)
                .ToListAsync();
        }

        //public async Task<Internship?> GetByIdAsync(int id)
        //{
        //    return await _context.Internships.FindAsync(id);
        //}
        public async Task<Internship?> GetByIdAsync(int id)
        {
            return await _context.Internships
                .Include(i => i.Company)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Internship internship)
        {
            await _context.Internships.AddAsync(internship);
        }

        public async Task DeleteAsync(Internship internship)
        {
            _context.Internships.Remove(internship);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
