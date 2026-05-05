using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetbyIdAsync(int id);
        Task AddAsync(Employee emp);
        Task UpdateAsync(Employee emp);
        Task DeleteAsync(int id);
        Task<bool> ExistByNameAsync(string name);
        

    }
}
