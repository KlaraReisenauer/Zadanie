using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieAPI.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static readonly EmployeeDTO[] _tmpEmployees = new[]
        {
            new EmployeeDTO
            {
                Id = new Guid("BBF73A4D-BF6E-4860-BDBE-66ADDD697012"),
                Name = "Harry",
                Surname = "Potter",
                Address = "4 Privet Drive, Surrey",
                DateOfBirth = new DateTime(1980, 7, 31),
                Salary = 2500.00,
                StartDate = new DateTime(2021, 11, 19), //TODO: mapping to adding time as well?? mappings using ninject?
                PositionId =  1
            },
            new EmployeeDTO
            {
                Id = new Guid("633F0C41-BC3A-4674-A94F-E24531724EDF"),
                Name = "Ron",
                Surname = "Weasley",
                Address = "The Burrow, on the outskirts of Ottery St Catchpole, Devon",
                DateOfBirth = new DateTime(1980, 3, 1),
                Salary = 2200.00,
                StartDate = new DateTime(2021, 11, 19), //TODO: mapping to adding time as well?? mappings using ninject?
                PositionId =  2
            },
            new EmployeeDTO
            {
                Id = new Guid("B3A11DB7-7E23-4775-9EBA-B5C98DF78401"),
                Name = "Hermione",
                Surname = "Granger",
                Address = "Heathgate and Meadway in Hampstead Garden Suburb, northwest London",
                DateOfBirth = new DateTime(1979, 9, 19),
                Salary = 3100.00,
                StartDate = new DateTime(2021, 11, 19), //TODO: mapping to adding time as well?? mappings using ninject?
                PositionId =  5
            }
        };

        public void Add(EmployeeDTO employee)
        {
            throw new NotImplementedException();
        }

        public IList<EmployeeDTO> GetAll()
        {
            return _tmpEmployees; //TODO: Replace with proper logic -> tmpEmployees are used only for testing purposes
        }

        public EmployeeDTO GetById(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public void RemoveAndArchive(Guid employeeId, DateTime removedOn)
        {
            throw new NotImplementedException();
        }

        public void Save(EmployeeDTO employee)
        {
            throw new NotImplementedException();
        }
    }
}
