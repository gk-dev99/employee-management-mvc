using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _dbContext;
        public EmployeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetbyIdAsync(int id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(f => f.Id == id);
        }
       

        public async Task AddAsync(Employee emp)
        {
            _dbContext.Employees.Add(emp);
             await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee emp)
        {
            var existingEmp = await _dbContext.Employees.FirstOrDefaultAsync(f => f.Id == emp.Id);
            if (existingEmp != null)
            {
                existingEmp.Name = emp.Name;
                existingEmp.Age = emp.Age;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var emp = await _dbContext.Employees.FirstOrDefaultAsync(f=> f.Id == id);
            if (emp != null)
            {
                _dbContext.Employees.Remove(emp);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _dbContext.Employees.AnyAsync(x=> x.Name == name);
        }
       
    }
}
