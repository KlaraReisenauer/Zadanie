using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZadanieAPI.Data.DTOs;

namespace ZadanieAPI.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public IList<Employee> GetAll();

        public Employee GetById(Guid id);

        public Employee Save(Employee employee);

        public bool Remove(Guid id, bool? removePermanently);
    }
}
