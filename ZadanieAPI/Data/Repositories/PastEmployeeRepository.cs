using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieAPI.Data.Repositories
{
    public class PastEmployeeRepository : IPastEmployeeRepository
    {
        public IList<PastEmployeeDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public PastEmployeeDTO GetById(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
