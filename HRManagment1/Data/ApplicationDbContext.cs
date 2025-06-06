using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HRManagment1.Models;

namespace HRManagment1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HRManagment1.Models.Applicant> Applicant { get; set; } = default!;
        public DbSet<HRManagment1.Models.Department> Department { get; set; } = default!;
        public DbSet<HRManagment1.Models.Employee> Employee { get; set; } = default!;
        public DbSet<HRManagment1.Models.EmployeeJobPosition> EmployeeJobPosition { get; set; } = default!;
        public DbSet<HRManagment1.Models.JobOpening> JobOpening { get; set; } = default!;
        public DbSet<HRManagment1.Models.JobPerformance> JobPerformance { get; set; } = default!;
        public DbSet<HRManagment1.Models.JobPositions> JobPositions { get; set; } = default!;
        public DbSet<HRManagment1.Models.LeaveRecord> LeaveRecord { get; set; } = default!;
        public DbSet<HRManagment1.Models.Payroll> Payroll { get; set; } = default!;
        public DbSet<HRManagment1.Models.TrainingRecords> TrainingRecords { get; set; } = default!;
    }
}
