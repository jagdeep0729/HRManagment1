using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagment1.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollId { get; set; }
        public String PayPeriod { get; set; }
        public float GrossPay { get; set; }
        public float Deducations { get; set; }
        public float NetPay { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
