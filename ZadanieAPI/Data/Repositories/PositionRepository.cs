using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieAPI.Data.DTOs;
using ZadanieAPI.Data.Repositories.Interfaces;

namespace ZadanieAPI.Data.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        public IList<Position> GetAll()
        {
            throw new NotImplementedException();
        }

        public Position GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Position Save(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
