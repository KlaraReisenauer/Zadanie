using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieAPI.Data.Repositories
{
    public class PositionsRepository : IPositionsRepository
    {
        public IList<PositionDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public PositionDTO GetById(Guid positionId)
        {
            throw new NotImplementedException();
        }

        public void Save(EmployeeDTO position)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid positionId)
        {
            throw new NotImplementedException();
        }

        public void Add(PositionDTO position)
        {
            throw new NotImplementedException();
        }
    }
}
