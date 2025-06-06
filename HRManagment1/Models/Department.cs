using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public String Name { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public ICollection<JobPositions> JobPosition { get; set; }
    }
}
