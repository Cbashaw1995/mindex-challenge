using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    /// Repository interface for managing Compensation records.
    public interface ICompensationRepository
    {
        // Adds a new compensation record
        void Add(Compensation compensation);

        // Retrieves the compensation for an employee
        Compensation GetByEmployeeId(string employeeId);

        // Saves changes to the repository
        void SaveChanges();
    }
}
