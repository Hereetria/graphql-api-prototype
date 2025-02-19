using GraphQL_Test_Project.Dtos;
using GraphQL_Test_Project.Schema.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GraphQL_Test_Project.Services
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) 
        {
        }

        public DbSet<CourseType> Courses { get; set; }
        public DbSet<InstructorType> Instructor { get; set; }
        public DbSet<StudentType> Student { get; set; }
    }
}
