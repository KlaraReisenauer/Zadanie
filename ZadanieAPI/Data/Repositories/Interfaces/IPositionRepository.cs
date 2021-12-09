using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieAPI.Data.DTOs;

namespace ZadanieAPI.Data.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        public IList<Position> GetAll();

        public Position GetById(int id);

        public Position Save(Position position);

        public bool Remove(int id);
    }
}
