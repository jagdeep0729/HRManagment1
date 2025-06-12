using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class JobPositions
    {
        [Key]   
        public int PositionId  { get; set; }

        public String Title { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public String SalaryGrade { get; set; }
        public ICollection<JobOpening> JobOpening { get; set; }
        public ICollection<EmployeeJobPosition> EmployeeJobPosition { get; set; }
    }
}
