using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public DateOnly HireDate { get; set; }

        public ICollection<Payroll> Payroll { get; set; }
        public ICollection<EmployeeJobPosition> EmployeeJobPosition { get; set; }
        public ICollection<TrainingRecords> TrainingRecord { get; set; }
        public ICollection<JobPerformance> JobPerformance { get; set; }
    }
}
