using System;
using System.Collections.Generic;
using System.Linq;
using ZadanieAPI.Database.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Repositories
{
    public class PastEmployeeRepository : IPastEmployeeRepository
    {
        private readonly CoreDbContext _dbContext;

        public PastEmployeeRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Public Methods
        public void ArchivateEmployee(Employee employee)
        {
            employee.EndDate = DateTime.Today;
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
        }

        public IList<Employee> GetAll()
        {
            return _dbContext.Employees.Where(e => e.EndDate != null)
                .OrderBy(e => e.Name).ToList();
        }

        public Employee GetById(Guid id)
        {
            var result = _dbContext.Employees.FirstOrDefault(e => e.EndDate != null
                && e.EmployeeId == id);
                
            return result == default(Employee) ? 
                throw new Exception($"Past employee with id {id} does not exists in current scope.")
                : result;
        }

        public bool Remove(Guid id)
        {
            Employee employeeToRemove = GetById(id) ?? new Employee();
            if(employeeToRemove != default(Employee))
            {
                var result = _dbContext.Employees.Remove(employeeToRemove);
                _dbContext.SaveChanges();
            }

            return true;
        }

        #endregion Public Methods


    }
}
