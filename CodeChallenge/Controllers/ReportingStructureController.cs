using Microsoft.AspNetCore.Mvc;
using CodeChallenge.Models;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingstructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /*
         * Handles HTTP GET request to compute the total number of reports for an employee.
         * - Retrieves an employee by ID.
         * - Counts all direct and indirect reports.
         * - Returns 404 Not Found if employee does not exist.
         */
        [HttpGet("{employeeId}")]
        public IActionResult GetReportingStructure(string employeeId)
        {
            var employee = _employeeService.GetById(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var reportingStructure = new ReportingStructure(employee, GetTotalReports(employee));
            return Ok(reportingStructure);
        }
        private int GetTotalReports(Employee employee)
        {
            if (employee?.DirectReports == null)
            {
                return 0;
            }

            int count = employee.DirectReports.Count;
            foreach (var directReport in employee.DirectReports)
            {
                var detailedEmployee = _employeeService.GetById(directReport.EmployeeId);
                count += GetTotalReports(detailedEmployee);
            }
            return count;
        }

    }
}
