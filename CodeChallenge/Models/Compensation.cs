using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Models
{
    /// Represents an employee's compensation details, including salary and effective date.
    public class Compensation
    {
        [Key] // Unique identifier for Compensation (Primary Key)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// Unique identifier of the employee associated with the compensation.
        [Required]
        public string EmployeeId { get; set; }  // Store EmployeeId directly to prevent EF errors

        /// Navigation property linking to the Employee entity.
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        /// The salary amount for the employee.
        public decimal Salary { get; set; }

        /// The date when the compensation became effective.
        public DateTime EffectiveDate { get; set; }
    }
}
