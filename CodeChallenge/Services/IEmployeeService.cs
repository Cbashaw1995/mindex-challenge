using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    /// Interface defining operations for managing Employee entities and their reporting structure.
    public interface IEmployeeService
    {
        /// Retrieves an employee by their unique ID.
        Employee GetById(String id);

        /// Creates a new employee record.
        Employee Create(Employee employee);

        /// Replaces an existing employee record with a new one.
        Employee Replace(Employee originalEmployee, Employee newEmployee);


        // reporting structure integration
        /// Retrieves the reporting structure of an employee.
        ReportingStructure GetReportingStructure(String employeeID);
    }
}
