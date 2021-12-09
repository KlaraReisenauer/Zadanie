using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieAPI.Data.DTOs;
using ZadanieAPI.Data.Repositories.Interfaces;

namespace ZadanieAPI.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IList<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id, bool? removePermanently)
        {
            throw new NotImplementedException();
        }

        public Employee Save(Employee employee)
        {
            if (employee.Id == null || employee.Id == Guid.Empty)
            {
                return AddNewEmployee(employee);
            }

            return EditEmployee(employee);
        }

        private Employee AddNewEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        private Employee EditEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
