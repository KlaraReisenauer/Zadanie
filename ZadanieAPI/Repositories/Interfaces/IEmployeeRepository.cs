using System;
using System.Collections.Generic;
using ZadanieAPI.Models;

namespace ZadanieAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        IList<EmployeeDTO> GetAll();

        EmployeeDTO GetById(Guid id);

        EmployeeDTO Save(EmployeeDTO employee);

        bool Remove(Guid id, bool removePermanently);

        bool CheckEmployeeEmpty(EmployeeDTO employee);
    }
}
