using System;
using System.Collections.Generic;
using ZadanieAPI.Models;

namespace ZadanieAPI.Repositories.Interfaces
{
    public interface IPastEmployeeRepository
    {
        IList<PastEmployeeDTO> GetAll();

        PastEmployeeDTO GetById(Guid id);

        bool Remove(Guid id);

        void ArchivateEmployee(PastEmployeeDTO pastEmployee);

    }
}
