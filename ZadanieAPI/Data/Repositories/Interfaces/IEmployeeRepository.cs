using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieAPI.Data.Repositories
{
    public interface IEmployeeRepository
    {

        /// <summary>
        /// Retrieve all actual employees from database
        /// </summary>
        /// <returns>List of employees</returns>
        public IList<EmployeeDTO> GetAll();

        /// <summary>
        /// Retrieve single employee from database based on passed id
        /// </summary>
        /// <param name="employeeId">ID identifying wanted employee</param>
        /// <returns>Employee data</returns>
        public EmployeeDTO GetById(Guid employeeId);

        /// <summary>
        /// Save changes to existing employee
        /// </summary>
        /// <param name="employee">employee data</param>
        public void Save(EmployeeDTO employee);

        /// <summary>
        /// Remove permanently employee from the database
        /// </summary>
        /// <param name="employeeId">Id identifying employee, which is to be removed</param>
        public void Remove(Guid employeeId);

        /// <summary>
        /// Move current employee to Past employees by setting EndDate on given record in database
        /// </summary>
        /// <param name="employeeId">Id identifying employee, which was removed</param>
        /// <param name="removedOn">Datetime stamp identifying when employee was removed</param>
        public void RemoveAndArchive(Guid employeeId, DateTime removedOn);

        /// <summary>
        /// Add new employee to database
        /// </summary>
        /// <param name="employee">New employee data</param>
        public void Add(EmployeeDTO employee);
    }
}
