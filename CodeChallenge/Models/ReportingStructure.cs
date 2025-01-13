using System;
namespace CodeChallenge.Models
{

    /// Represents the reporting structure of an employee, including the total number of reports.

    public class ReportingStructure
    {
        /// The employee whose reporting structure is being calculated.
        public Employee Employee { get; set; }

        /// The total number of direct and indirect reports under the employee.
        public int NumberOfReports { get; set; }


        /// Constructor for ReportingStructure.
        public ReportingStructure(Employee employee, int numberOfReports)
        {
            Employee = employee;
            NumberOfReports = numberOfReports;
        }
    }
}
