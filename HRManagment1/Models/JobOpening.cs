using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class JobOpening
    {
        [Key]
        public int JobOpeningId { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public JobPositions JobPositions { get; set; }
        public DateOnly OpenDate { get; set; }
        public DateOnly CloseDate { get; set; }
        public String Status { get; set; }
        public ICollection<JobOpeningApplicant> JobOpeningApplicant { get; set; }
    }
}
