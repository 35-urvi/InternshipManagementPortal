using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInternshipPortal.Domain.Entities;

namespace UniversityInternshipPortal.Application.Interfaces
{
    public interface IApplicationRepository : IGenericRepository<InternshipApplication>
    {
        Task<IEnumerable<InternshipApplication>> GetApplicationsByStudent(int studentId);

        Task<IEnumerable<InternshipApplication>> GetApplicationsByInternship(int internshipId);
    }
}
