using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        // Adds a new employee to the database
        Employee Add(Employee employee);

        // Retrieves an employee by their unique ID
        Employee GetById(string id);

        // Saves changes asynchronously to the database
        Task SaveAsync();

        // Removes an employee from the database
        Employee Remove(Employee employee);
    }
}
