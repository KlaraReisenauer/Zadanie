using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieAPI.Data.Repositories
{
    public interface IPastEmployeeRepository
    {

        /// <summary>
        /// Retrieve all past employees from database
        /// </summary>
        /// <returns>List of employees</returns>
        public IList<PastEmployeeDTO> GetAll();

        /// <summary>
        /// Retrieve single employee from database based on passed id
        /// </summary>
        /// <param name="employeeId">ID identifying wanted employee</param>
        /// <returns>Employee data</returns>
        public PastEmployeeDTO GetById(Guid employeeId);

        /// <summary>
        /// Remove past employee permanently from database
        /// </summary>
        /// <param name="employeeId">Id identifying employee, which is to be removed</param>
        public void Remove(Guid employeeId);
    }
}
