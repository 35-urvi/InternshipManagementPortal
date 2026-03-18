using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using UniversityInternshipPortal.Domain.Entities;

namespace UniversityInternshipPortal.Infrastructure.Data
{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<User> Users { get; set; }

            public DbSet<Student> Students { get; set; }

            public DbSet<Company> Companies { get; set; }

            public DbSet<Faculty> Faculties { get; set; }

            public DbSet<Internship> Internships { get; set; }

            public DbSet<InternshipApplication> Applications { get; set; }

            public DbSet<InternshipApplication> InternshipApplications { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<InternshipApplication>()
                    .HasOne(a => a.Student)
                    .WithMany()
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<InternshipApplication>()
                    .HasOne(a => a.Internship)
                    .WithMany()
                    .HasForeignKey(a => a.InternshipId)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
