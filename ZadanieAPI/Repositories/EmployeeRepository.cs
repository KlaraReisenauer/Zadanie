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
        private IList<EmployeeDTO> _employees = new List<EmployeeDTO>()
        {
            new EmployeeDTO
            {
                Id = new Guid("633F0C41-BC3A-4674-A94F-E24531724EDF"),
                Name = "Ronald",
                Surname = "Weasley",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 2,
            },
            new EmployeeDTO
            {

                Id = new Guid("B3A11DB7-7E23-4775-9EBA-B5C98DF78401"),
                Name = "Hermione",
                Surname = "Granger",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 5,
            },
            new EmployeeDTO
            {
                Id = new Guid("BBF73A4D-BF6E-4860-BDBE-66ADDD697012"),
                Name = "Harry",
                Surname = "Potter",
                Address = "4 Privet Drive, Surrey",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 2500.55,
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

        public IList<EmployeeDTO> GetAll()
        {
            var employees = _dbContext.Employees.Where(e => e.EndDate == null).ToList();

            //TODO: remove when above working
            return _employees;
        }

        public EmployeeDTO GetById(Guid id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);

            //TODO: remove when above working
            EmployeeDTO empl = _employees.FirstOrDefault(e => e.Id == id);
            return empl == default(EmployeeDTO)
                ? throw new Exception($"Employee with id {id} does not exist in current scope")
                : empl;
        }

        public bool Remove(Guid id, bool removePermanently)
        {
            //TODO: remove when above working
            EmployeeDTO emplToRemove = _employees.FirstOrDefault(e => e.Id == id);

            if (emplToRemove == default(EmployeeDTO))
            {
                if (!removePermanently)
                { //check if wasnt already moved into past employees
                    PastEmployeeDTO pastEmpl = new PastEmployeeDTO();

                    try
                    {
                        pastEmpl = _pastEmployeeRepository.GetById(id);

                    }
                    catch (Exception) { }

                    if (CheckEmployeeEmpty(pastEmpl))
                    {
                        return false;
                    }
                }

                return true;
            }

            if (!removePermanently)
            {
                _pastEmployeeRepository.ArchivateEmployee(
                    new PastEmployeeDTO
                    {
                        Name = emplToRemove.Name,
                        Surname = emplToRemove.Surname,
                        Address = emplToRemove.Address,
                        DateOfBirth = emplToRemove.DateOfBirth,
                        PositionId = emplToRemove.PositionId,
                        Salary = emplToRemove.Salary,
                        StartDate = emplToRemove.StartDate // TODO: alebo nahradim bez kontroly??
                    });
            }

            return _employees.Remove(emplToRemove);
        }

        public EmployeeDTO Save(EmployeeDTO employee)
        {
            if (employee.Id == null || employee.Id == Guid.Empty)
            {
                return AddNewEmployee(employee);
            }

            return EditEmployee(employee);
        }

        public bool CheckEmployeeEmpty(EmployeeDTO employee)
        {
            //TODO: remove when above working
            return string.IsNullOrEmpty(employee.Name) &&
                string.IsNullOrEmpty(employee.Surname) &&
                employee.PositionId <= 0 &&
                employee.Salary == 0 &&
                employee.DateOfBirth == default(DateTime);
        }

        private EmployeeDTO AddNewEmployee(EmployeeDTO employee)
        {
            //TODO: remove when above working
            _employees.Add(employee); // TODO: get new id from db after saving "newId = ..."
            return employee;
        }

        private EmployeeDTO EditEmployee(EmployeeDTO employee)
        {
            //TODO: remove when above working
            EmployeeDTO emplToEdit = GetById(employee.Id.Value);

            emplToEdit.Name = employee.Name;
            emplToEdit.Surname = employee.Surname;
            emplToEdit.Address = employee.Address;
            emplToEdit.DateOfBirth = employee.DateOfBirth;
            emplToEdit.PositionId = employee.PositionId;
            emplToEdit.Salary = employee.Salary;
            emplToEdit.StartDate = employee.StartDate;

            return emplToEdit;
        }
    }
}
