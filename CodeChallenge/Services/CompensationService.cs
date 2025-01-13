using CodeChallenge.Models;
using CodeChallenge.Repositories;
using System;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        public CompensationService(ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
        }
        public Compensation Create(Compensation compensation)
        {
            if (compensation == null || string.IsNullOrEmpty(compensation.EmployeeId))
            {
                throw new ArgumentException("Compensation or EmployeeId cannot be null.");
            }

            var existingCompensation = _compensationRepository.GetByEmployeeId(compensation.EmployeeId);
            if (existingCompensation != null)
            {
                return existingCompensation; // Return the existing record instead of failing
            }

            _compensationRepository.Add(compensation);
            _compensationRepository.SaveChanges();
            return compensation;
        }





        public Compensation GetByEmployeeId(string employeeId)
        {
            return _compensationRepository.GetByEmployeeId(employeeId);
        }
    }
}
