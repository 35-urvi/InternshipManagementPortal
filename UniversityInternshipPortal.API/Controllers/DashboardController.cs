using Microsoft.AspNetCore.Mvc;
using UniversityInternshipPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UniversityInternshipPortal.API.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class DashboardController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public DashboardController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet("stats")]
            public async Task<IActionResult> GetStats()
            {
                var totalInternships = await _context.Internships.CountAsync();

                var totalApplications = await _context.InternshipApplications.CountAsync();

                var acceptedApplications = await _context.InternshipApplications
                    .Where(a => a.Status == "Accepted")
                    .CountAsync();

                var acceptanceRate = totalApplications == 0
                    ? 0
                    : (double)acceptedApplications / totalApplications * 100;

                return Ok(new
                {
                    totalInternships,
                    totalApplications,
                    acceptedApplications,
                    acceptanceRate
                });
            }

            [HttpGet("applications-by-department")]
            public async Task<IActionResult> GetApplicationsByDepartment()
            {
                var data = await _context.InternshipApplications
                    .Include(a => a.Student)
                    .GroupBy(a => a.Student.Department)
                    .Select(g => new
                    {
                        department = g.Key,
                        count = g.Count()
                    })
                    .ToListAsync();

                return Ok(data);
            }
        }
}
