using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public String Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<JobPositions> JobPositions { get; set; }
    }

}