using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CodeChallenge.Repositories
{
    /// Repository implementation for managing Compensation records.
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _context;

        /// Constructor to inject the database context.
        public CompensationRepository(EmployeeContext context)
        {
            _context = context;
        }

        // Adds a new compensation entry to the database
        public void Add(Compensation compensation)
        {
            _context.Compensations.Add(compensation);
        }

        // Retrieves a compensation entry by employee ID
        public Compensation GetByEmployeeId(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
            {
                throw new ArgumentException("EmployeeId cannot be null.");
            }

            return _context.Compensations
                .Include(c => c.Employee)
                .FirstOrDefault(c => c.EmployeeId == employeeId);
        }

        // Saves changes to the database
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
