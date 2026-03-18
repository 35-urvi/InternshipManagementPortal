using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UniversityInternshipPortal.Application.Interfaces;
using UniversityInternshipPortal.Domain.Entities;
using UniversityInternshipPortal.Infrastructure.Data;

namespace UniversityInternshipPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternshipController : ControllerBase
    {
        private readonly IInternshipRepository _internshipRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ApplicationDbContext _context;

        public InternshipController(IInternshipRepository internshipRepository,ICompanyRepository companyRepository, ApplicationDbContext context)
        {
            _internshipRepository = internshipRepository;
            _companyRepository = companyRepository;
            _context = context;
        }

        [Authorize(Roles = "Company")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateInternship(Internship internship)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var company = await _companyRepository.GetByUserIdAsync(userId);

            if (company == null)
                return BadRequest("Company profile not found");

            internship.CompanyId = company.Id;

            await _internshipRepository.AddAsync(internship);
            await _internshipRepository.SaveAsync();

            return Ok(new { message = "Internship created successfully" });
        }

        // STUDENTS VIEW INTERNSHIPS
        //[AllowAnonymous]
        //[HttpGet("all")]
        //public async Task<IActionResult> GetAllInternships()
        //{
        //    var internships = await _internshipRepository.GetAllAsync();
        //    return Ok(internships);
        //}
        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllInternships()
        {
            var internships = await _context.Internships
                .Select(i => new
                {
                    i.Id,
                    i.Title,
                    i.Description,
                    i.RequiredSkills,
                    i.Stipend,
                    i.CompanyId,
                    CompanyName = _context.Companies
                        .Where(c => c.Id == i.CompanyId)
                        .Select(c => c.CompanyName)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(internships);
        }

        // GET INTERNSHIP BY ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInternship(int id)
        {
            var internship = await _internshipRepository.GetByIdAsync(id);

            if (internship == null)
                return NotFound();

            return Ok(internship);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchInternships(
    string? keyword,
    string? skill,
    int? minStipend)
        {
            var internships = await _internshipRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(keyword))
            {
                internships = internships
                    .Where(i =>
                        i.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        i.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(skill))
            {
                internships = internships
                    .Where(i =>
                        i.RequiredSkills.Contains(skill, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (minStipend.HasValue)
            {
                internships = internships
                    .Where(i => i.Stipend >= minStipend.Value)
                    .ToList();
            }

            return Ok(internships);
        }

        [Authorize(Roles = "Company")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateInternship(int id, Internship updatedInternship)
        {
            var internship = await _internshipRepository.GetByIdAsync(id);

            if (internship == null)
                return NotFound("Internship not found");

            internship.Title = updatedInternship.Title;
            internship.Description = updatedInternship.Description;
            internship.RequiredSkills = updatedInternship.RequiredSkills;
            internship.Stipend = updatedInternship.Stipend;

            await _internshipRepository.SaveAsync();

            return Ok(new { message = "Internship updated successfully" });
        }

        [Authorize(Roles = "Company")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteInternship(int id)
        {
            var internship = await _internshipRepository.GetByIdAsync(id);

            if (internship == null)
                return NotFound("Internship not found");

            await _internshipRepository.DeleteAsync(internship);
            await _internshipRepository.SaveAsync();

            return Ok(new { message = "Internship deleted successfully" });
        }

    }
}
