using System.Collections.Generic;
using ZadanieAPI.Database.Models;
using ZadanieAPI.Models;

namespace ZadanieAPI.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        IList<Position> GetAll();

        Position GetById(int id);

        Position Save(Position position);

        bool Remove(int id);
    }
}
