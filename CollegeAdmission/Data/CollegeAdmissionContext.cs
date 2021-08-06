using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CollegeAdmission.Models;

namespace CollegeAdmission.Data
{
    public class CollegeAdmissionContext : DbContext
    {
        public CollegeAdmissionContext (DbContextOptions<CollegeAdmissionContext> options)
            : base(options)
        {
        }
        public DbSet<Course> Course { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Login> Login { get; set; }
    }
}
