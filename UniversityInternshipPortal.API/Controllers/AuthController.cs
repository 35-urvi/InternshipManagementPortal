using Microsoft.AspNetCore.Mvc;
using UniversityInternshipPortal.API.Auth;
using UniversityInternshipPortal.Domain.Entities;
using UniversityInternshipPortal.Infrastructure.Data;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace UniversityInternshipPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudent(RegisterStudentRequest request)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = "Student"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var student = new Student
            {
                FullName = request.Name,
                Department = request.Department,
                CGPA = request.CGPA,
                UserId = user.Id
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student registered successfully" });
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("Invalid email");

            bool verified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!verified)
                return Unauthorized("Invalid password");

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

            return Ok(new { token });
        }

        [HttpPost("register-company")]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyRequest request)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = "Company"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var company = new Company
            {
                CompanyName = request.CompanyName,
                Industry = request.Industry,
                UserId = user.Id
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Company registered successfully" });
        }

    }
}
