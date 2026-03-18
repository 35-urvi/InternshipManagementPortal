using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UniversityInternshipPortal.Infrastructure.Data;

namespace UniversityInternshipPortal.API.Controllers
{
    [ApiController]
    [Route("api/student")]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [HttpGet("my-applications")]
        public async Task<IActionResult> GetMyApplications()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
                return BadRequest("Student profile not found");

            var applications = await _context.InternshipApplications
                .Include(a => a.Internship)
                .Where(a => a.StudentId == student.Id)
                .ToListAsync();

            return Ok(applications);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("recommended-internships")]
        public async Task<IActionResult> GetRecommendedInternships()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
                return BadRequest("Student profile not found");

            var internships = await _context.Internships.ToListAsync();

            var recommended = internships
                .Where(i =>
                    i.RequiredSkills.Contains(student.Department) ||
                    i.Title.Contains(student.Department))
                .Take(10)
                .ToList();

            return Ok(recommended);
        }
    }
}
