using System;
using System.Collections.Generic;
using ZadanieAPI.Database.Models;

namespace ZadanieAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        IList<Employee> GetAll();

        //Employee GetById(Guid id);

        Employee Save(Employee employee);

        bool Remove(Guid id, bool removePermanently);
    }
}
