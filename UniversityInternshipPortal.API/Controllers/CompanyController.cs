using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityInternshipPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UniversityInternshipPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Company")]
        [HttpGet("applications/{internshipId}")]
        public async Task<IActionResult> GetApplications(int internshipId)
        {
            var applications = await _context.InternshipApplications
                .Include(a => a.Student)
                .Where(a => a.InternshipId == internshipId)
                .Select(a => new
                {
                    ApplicationId = a.Id,
                    StudentId = a.StudentId,
                    StudentName = a.Student.FullName,
                    Department = a.Student.Department,
                    CGPA = a.Student.CGPA,
                    Status = a.Status
                })
                .ToListAsync();

            return Ok(applications);
        }

        
        [Authorize(Roles = "Company")]
        [HttpPut("application/accept/{id}")]
        public async Task<IActionResult> AcceptApplication(int id)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var application = await _context.InternshipApplications
                .Include(a => a.Internship)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
                return NotFound("Application not found");

            if (application.Internship.CompanyId != company.Id)
                return Forbid("You cannot manage applications for another company");

            application.Status = "Accepted";

            await _context.SaveChangesAsync();

            return Ok(new { message = "Application accepted" });
        }


        [Authorize(Roles = "Company")]
        [HttpPut("application/reject/{id}")]
        public async Task<IActionResult> RejectApplication(int id)
        {
            var application = await _context.InternshipApplications.FindAsync(id);

            if (application == null)
                return NotFound("Application not found");

            application.Status = "Rejected";

            await _context.SaveChangesAsync();

            return Ok(new { message = "Application rejected" });
        }

       
        [Authorize(Roles = "Company")]
        [HttpGet("my-internships")]
        public async Task<IActionResult> GetMyInternships()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (company == null)
                return BadRequest("Company profile not found");

            var internships = await _context.Internships
                .Where(i => i.CompanyId == company.Id)
                .ToListAsync();

            return Ok(internships);
        }


    }
}
