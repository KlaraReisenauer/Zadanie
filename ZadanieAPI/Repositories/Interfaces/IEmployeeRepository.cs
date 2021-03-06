using System;
using System.Collections.Generic;
using ZadanieAPI.Database.Models;

namespace ZadanieAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        IList<Employee> GetAll();

        Guid Save(Employee employee);

        bool Remove(Guid id, bool removePermanently);
    }
}
