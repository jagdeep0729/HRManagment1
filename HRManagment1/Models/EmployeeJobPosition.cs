using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class EmployeeJobPosition
    {
        [Key]
        public int RecordId { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public JobPositions JobPositions { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly Enddate { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
