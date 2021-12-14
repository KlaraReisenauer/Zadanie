using System;
using System.Collections.Generic;
using System.Linq;
using ZadanieAPI.Database.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Repositories
{
    public class PastEmployeeRepository : IPastEmployeeRepository
    {
        private IList<Employee> _pastEmployees = new List<Employee>()
        {
            new Employee
            {
                EmployeeId = new Guid("8F7DE5AC-769A-44EF-B9F6-1C3AAA0A219B"),
                Name = "Sirius",
                Surname = "Black",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 4,
                EndDate = new DateTime(1995, 1, 1)
            },
            new Employee
            {
                EmployeeId = new Guid("8C2164E5-5670-4733-B888-2D8F44F6F704"),
                Name = "Lily",
                Surname = "Potter",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 3,
                EndDate = new DateTime(1995, 1, 1)
            },
            new Employee
            {
                EmployeeId = new Guid("F58C8E6D-2AA7-4FEC-A647-EA336C70BC5F"),
                Name = "James",
                Surname = "Potter",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 3,
                EndDate = new DateTime(1995, 1, 1)
            }
        };
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
            var result = _dbContext.Employees.Where(e => e.EndDate != null).ToList();

            //TODO: remove when above working
            return _pastEmployees;
        }

        public Employee GetById(Guid id)
        {
            var result = _dbContext.Employees.FirstOrDefault(e => e.EndDate != null
                && e.EmployeeId == id);

            //TODO: remove when above working
            Employee pastEmployee = _pastEmployees.FirstOrDefault(e => e.EmployeeId == id);
            return pastEmployee == default(Employee)
                ? throw new Exception($"Past employee with id {id} does not exist in current scope")
                : pastEmployee;
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
            //TODO: remove when above working
            Employee emplToRemove = _pastEmployees
                .FirstOrDefault(e => e.EmployeeId == id);
            if (emplToRemove == default(Employee))
            {
                return true;
            }

            return _pastEmployees.Remove(emplToRemove);
        }

        #endregion Public Methods

    }
}
