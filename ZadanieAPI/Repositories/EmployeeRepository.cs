using System;
using System.Collections.Generic;
using System.Linq;
using ZadanieAPI.Database.Models;
using ZadanieAPI.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IPastEmployeeRepository _pastEmployeeRepository;
        private readonly CoreDbContext _dbContext;

        public EmployeeRepository(IPastEmployeeRepository pastEmployeeRepository,
            CoreDbContext dbContext)
        {
            _pastEmployeeRepository = pastEmployeeRepository;
            _dbContext = dbContext;
        }

        #region Public Methods

        public IList<Employee> GetAll()
        {
            return _dbContext.Employees.Where(e => e.EndDate == null)
                .OrderBy(e => e.Name).ToList();
        }

        public bool Remove(Guid id, bool removePermanently)
        {
            Employee emplToRemove = GetById(id) ?? new Employee();
            if (removePermanently)
            {
                return RemoveEmployeePermenently(emplToRemove);
            }
            else
            {
                return ArchivateEmployee(emplToRemove, id);
            }

        }

        public Guid Save(Employee employee)
        {
            if (employee.EmployeeId == Guid.Empty)
            {
                return AddNewEmployee(employee);
            }

            return EditEmployee(employee);
        }

        #endregion Public Methods

        #region Private Methods

        private Employee GetById(Guid id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);

            return employee == default(Employee)
                ? throw new Exception($"Employee with id {id} does not exist in current scope")
                : employee;
        }

        private Guid AddNewEmployee(Employee employee)
        {
            var newEmployee = _dbContext.Add(employee);
            _dbContext.SaveChanges();

            return newEmployee.Entity.EmployeeId;
        }

        private Guid EditEmployee(Employee employee)
        {
            var result = _dbContext.Employees.Update(employee);    
            _dbContext.SaveChanges();

            return employee.EmployeeId;
        }

        private bool ArchivateEmployee(Employee employee, Guid id)
        {
            if (employee == default(Employee))
            {
                Employee pastEmpl = _pastEmployeeRepository.GetById(id)
                    ?? new Employee();

                if (pastEmpl == default(Employee))
                {
                    return false;
                }
            }
            else
            {
                _pastEmployeeRepository.ArchivateEmployee(employee);
            }

            return true;
        }

        private bool RemoveEmployeePermenently(Employee employee)
        {
            if (employee != default(Employee))
            {
                var result = _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }

            return true;
        }

        #endregion Private Methods
    }
}
