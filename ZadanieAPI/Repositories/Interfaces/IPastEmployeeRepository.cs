using System;
using System.Collections.Generic;
using ZadanieAPI.Database.Models;
using ZadanieAPI.Models;

namespace ZadanieAPI.Repositories.Interfaces
{
    public interface IPastEmployeeRepository
    {
        IList<Employee> GetAll();

        Employee GetById(Guid id);

        bool Remove(Guid id);

        void ArchivateEmployee(Employee pastEmployee);

    }
}
