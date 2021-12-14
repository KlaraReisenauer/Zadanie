using System;
using System.Collections.Generic;
using System.Linq;
using ZadanieAPI.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Repositories
{
    public class PastEmployeeRepository : IPastEmployeeRepository
    {
        private IList<PastEmployeeDTO> _pastEmployees = new List<PastEmployeeDTO>()
        {
            new PastEmployeeDTO
            {
                Id = new Guid("8F7DE5AC-769A-44EF-B9F6-1C3AAA0A219B"),
                Name = "Sirius",
                Surname = "Black",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 4,
                EndDate = new DateTime(1995, 1, 1)
            },
            new PastEmployeeDTO
            {
                Id = new Guid("8C2164E5-5670-4733-B888-2D8F44F6F704"),
                Name = "Lily",
                Surname = "Potter",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 3,
                EndDate = new DateTime(1995, 1, 1)
            },
            new PastEmployeeDTO
            {
                Id = new Guid("F58C8E6D-2AA7-4FEC-A647-EA336C70BC5F"),
                Name = "James",
                Surname = "Potter",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 0,
                StartDate = new DateTime(1980, 7, 31),
                PositionId = 3,
                EndDate = new DateTime(1995, 1, 1)
            }
        };

        public void ArchivateEmployee(PastEmployeeDTO pastEmployee)
        {
            pastEmployee.EndDate = DateTime.Today;
            _pastEmployees.Add(pastEmployee);
        }

        public IList<PastEmployeeDTO> GetAll()
        {
            return _pastEmployees;
        }

        public PastEmployeeDTO GetById(Guid id)
        {
            PastEmployeeDTO pastEmployee = _pastEmployees.FirstOrDefault(e => e.Id == id);
            return pastEmployee == default(PastEmployeeDTO)
                ? throw new Exception($"Past employee with id {id} does not exist in current scope")
                : pastEmployee;
        }

        public bool Remove(Guid id)
        {
            PastEmployeeDTO emplToRemove = _pastEmployees.FirstOrDefault(e => e.Id == id);
            if (emplToRemove == default(EmployeeDTO))
            {
                return true;
            }

            return _pastEmployees.Remove(emplToRemove);
        }

    }
}
