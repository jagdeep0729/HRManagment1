using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Resume { get; set; }
        public DateOnly ApplicantDate { get; set; }
        public ICollection<JobOpeningApplicant> JobOpeningApplicant { get; set; }
    }
}
