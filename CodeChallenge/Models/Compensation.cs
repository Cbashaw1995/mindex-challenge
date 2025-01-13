using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        [Key] // Unique identifier for Compensation (Primary Key)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }  // Store EmployeeId directly to prevent EF errors

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
