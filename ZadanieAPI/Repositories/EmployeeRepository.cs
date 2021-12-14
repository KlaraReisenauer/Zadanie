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
        private IList<Employee> _employees = new List<Employee>()
        {
            new Employee
            {
                EmployeeId = new Guid("633F0C41-BC3A-4674-A94F-E24531724EDF"),
                Name = "Ronald",
                Surname = "Weasley",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 2,
            },
            new Employee
            {

                EmployeeId = new Guid("B3A11DB7-7E23-4775-9EBA-B5C98DF78401"),
                Name = "Hermione",
                Surname = "Granger",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 5,
            },
            new Employee
            {
                EmployeeId = new Guid("BBF73A4D-BF6E-4860-BDBE-66ADDD697012"),
                Name = "Harry",
                Surname = "Potter",
                Address = "4 Privet Drive, Surrey",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 2500.55M,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 1,
            }
        };

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
            var employees = _dbContext.Employees.Where(e => e.EndDate == null).ToList();

            //TODO: remove when above working
            return _employees;
        }

        public Employee GetById(Guid id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);

            //TODO: remove when above working
            Employee empl = _employees.FirstOrDefault(e => e.EmployeeId == id);

            return empl == default(Employee)
                ? throw new Exception($"Employee with id {id} does not exist in current scope")
                : empl;
        }

        public bool Remove(Guid id, bool removePermanently)
        {
            //TODO: remove when above working
            Employee emplToRemove = GetById(id) ?? new Employee();
            if (removePermanently)
            {
                return RemoveEmployeePermenently(emplToRemove);
            }
            else
            {
                return ArchivateEmployee(emplToRemove);
            }

        }

        public Employee Save(Employee employee)
        {
            if (employee.EmployeeId == Guid.Empty)
            {
                return AddNewEmployee(employee);
            }

            return EditEmployee(employee);
        }

        #endregion Public Methods

        #region Private Methods

        private Employee AddNewEmployee(Employee employee)
        {
            var newEmployee = _dbContext.Add(employee);
            _dbContext.SaveChanges();// TODO: get new id from db after saving "newId = ..."

            //TODO: remove when above working
            _employees.Add(employee); 
            return employee;
        }

        private Employee EditEmployee(Employee employee)
        {
            var employeeToEdit = _dbContext.Employees
                .FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            var result = _dbContext.Employees.Update(employeeToEdit);
            _dbContext.SaveChanges();

            return employeeToEdit;
        }

        private bool ArchivateEmployee(Employee employee)
        {
            if (employee == default(Employee))
            {// TODO: UPRAVIT!!!
                Employee pastEmpl = _pastEmployeeRepository.GetById(employee.EmployeeId)
                    ?? new Employee();

                if(pastEmpl == default(Employee))
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
                var result = _dbContext.Employees.Remove(employee); //TODO: check result?
            }

            return true;
        }

        #endregion Private Methods
    }
}
