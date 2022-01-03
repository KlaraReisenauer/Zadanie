using System.Collections.Generic;
using ZadanieAPI.Database.Models;
using ZadanieAPI.Models;

namespace ZadanieAPI.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        IEnumerable<Position> GetAll();

        int Save(Position position);

        bool Remove(int id);
    }
}
