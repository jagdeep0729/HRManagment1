using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class LeaveRecord
    {

        [Key]
        public int LeaveId { get; set; }
        public String LeaveType { get; set; }
        public string Status { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
