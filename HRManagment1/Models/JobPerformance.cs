using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class JobPerformance
    {
        [Key]
        public int PerformanceId { get; set; }
        public DateOnly ReviewDate { get; set; }
        public float Score { get; set; }
        public String Reviewer { get; set; }
        public String Comments { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
