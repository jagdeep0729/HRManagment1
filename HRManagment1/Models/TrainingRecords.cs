using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class TrainingRecords
    {

        [Key]
        public int TrainingId { get; set; }
        public String TrainingName { get; set; }
        public String Institution { get; set; }
        public DateOnly CompletionDate { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
