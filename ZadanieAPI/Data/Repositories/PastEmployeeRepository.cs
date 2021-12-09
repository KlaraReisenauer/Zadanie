using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieAPI.Data.DTOs;
using ZadanieAPI.Data.Repositories.Interfaces;

namespace ZadanieAPI.Data.Repositories
{
    public class PastEmployeeRepository : IPastEmployeeRepository
    {
        public IList<PastEmployee> GetAll()
        {
            throw new NotImplementedException();
        }

        public PastEmployee GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
