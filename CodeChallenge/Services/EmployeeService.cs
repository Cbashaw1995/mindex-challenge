using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using System.Threading;

namespace CodeChallenge.Services
{
    /// Service implementation for managing Employee records and their reporting structure.
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        /// Constructor for injecting dependencies.
        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if (employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if (originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }


        /// Retrieves the reporting structure for a given employee ID.
        // takes in employee id and returns reporting structure containing total reports 

        public ReportingStructure GetReportingStructure(string employeeID)
        {
            var employee = _employeeRepository.GetById(employeeID);
            if (employee == null)
            {
                return null;
            }
            int numberOfReports = CountTotalReports(employee);
            return new ReportingStructure(employee, numberOfReports);
        }


        //recursivley counts all reports for an employee
        // takes in employee and returns total number of reports
        private int CountTotalReports(Employee employee)
        {
            if (employee.DirectReports == null || employee.DirectReports.Count == 0)
            {
                return 0; // BASE CASE
            }

            int totalReports = 0;
            Queue<string> queue = new Queue<string>();

            foreach (var directReport in employee.DirectReports)
            {
                var fullEmployee = _employeeRepository.GetById(directReport.EmployeeId);
                if (fullEmployee != null)
                {
                    queue.Enqueue(fullEmployee.EmployeeId);  // FIX: Enqueue employeeId, not the Employee object
                    totalReports++;
                }
            }

            while (queue.Count > 0)
            {
                var currentEmployeeId = queue.Dequeue();
                var currentEmployee = _employeeRepository.GetById(currentEmployeeId);

                if (currentEmployee != null && currentEmployee.DirectReports != null)
                {
                    foreach (var indirectReport in currentEmployee.DirectReports)
                    {
                        var fullIndirectEmployee = _employeeRepository.GetById(indirectReport.EmployeeId);
                        if (fullIndirectEmployee != null)
                        {
                            queue.Enqueue(fullIndirectEmployee.EmployeeId);  // FIX: Enqueue employeeId
                            totalReports++;
                        }
                    }
                }
            }

            return totalReports;
        }
    }
}
