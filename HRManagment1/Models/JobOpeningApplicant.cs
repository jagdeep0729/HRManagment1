using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagment1.Models
{
    public class JobOpeningApplicant
    {
        [Key]
        public int JobOpeningApplicantId { get; set; }
        public int ApplicantId { get; set; }
        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }
        public int JobOpeningId { get; set; }
        [ForeignKey("JobOpeningId")]
        public JobOpening JobOpening { get; set; }  
    }
}
