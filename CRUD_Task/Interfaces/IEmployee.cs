using Azure;
using CRUD_Task.Models;

namespace CRUD_Task.Interfaces
{
    public interface IEmployee
    {
        Task<IEnumerable<Models.Employee>> Get();
        Task<Employee>Find(int id);
        Task<Employee> Add(Employee model);
        Task<Employee>Update(Employee model);
        Task<Employee>Remove(Employee model);

    }
}
