using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityInternshipPortal.Application.DTOs;
using UniversityInternshipPortal.Application.Interfaces;
using UniversityInternshipPortal.Domain.Entities;
using UniversityInternshipPortal.Infrastructure.Data;

namespace UniversityInternshipPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        [Authorize(Roles = "Student")]
        [HttpPost("apply")]
        public async Task<IActionResult> Apply(int internshipId)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
                return BadRequest("Student profile not found");

            var existingApplication = await _context.InternshipApplications
                .FirstOrDefaultAsync(a =>
                    a.StudentId == student.Id &&
                    a.InternshipId == internshipId);

            if (existingApplication != null)
                return BadRequest("You already applied for this internship");

            var application = new InternshipApplication
            {
                StudentId = student.Id,
                InternshipId = internshipId,
                Status = "Pending"
            };

            _context.InternshipApplications.Add(application);

            await _context.SaveChangesAsync();

            return Ok("Application submitted successfully");
        }


    }
}
