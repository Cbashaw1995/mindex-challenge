using Microsoft.AspNetCore.Mvc;
using CodeChallenge.Models;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{    
    /// API Controller for managing Compensation-related operations.
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ICompensationService _compensationService;

        /// Constructor to inject dependencies.
        public CompensationController(ICompensationService compensationService)
        {
            _compensationService = compensationService;
        }

        /*
         * Handles HTTP POST request to create a new compensation record.
         * - Receives compensation details in the request body.
         * - Validates if the employee exists and prevents duplicate entries.
         * - Returns a 201 Created response with the new compensation record.
         */
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            if (compensation == null)
            {
                return BadRequest("Compensation object cannot be null.");
            }

            if (string.IsNullOrEmpty(compensation.EmployeeId))
            {
                return BadRequest("Employee ID cannot be empty.");
            }

            var existingCompensation = _compensationService.GetByEmployeeId(compensation.EmployeeId);
            if (existingCompensation != null)
            {
                return Ok(existingCompensation);  // Instead of returning Conflict, return existing Compensation
            }

            var createdCompensation = _compensationService.Create(compensation);
            return CreatedAtAction(nameof(GetCompensation), new { employeeId = createdCompensation.EmployeeId }, createdCompensation);
        }



        /*
         * Handles HTTP GET request to retrieve a compensation record.
         * - Uses employeeId to look up compensation.
         * - Returns 404 Not Found if no record exists.
         * - Returns 200 OK with compensation details if found.
         */
        [HttpGet("{employeeId}")]
        public IActionResult GetCompensation(string employeeId)
        {
            var compensation = _compensationService.GetByEmployeeId(employeeId);
            if (compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);
        }
    }
}
