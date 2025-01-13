using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRepository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        // Adds a new employee to the database
        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        // Retrieves an employee by their unique ID
        public Employee GetById(string id)
        {
            return _employeeContext.Employees
                        .Include(e => e.DirectReports)  // Ensures DirectReports are included
                        .SingleOrDefault(e => e.EmployeeId == id);
        }

        // Saves changes asynchronously to the database
        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        // Removes an employee from the database
        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
