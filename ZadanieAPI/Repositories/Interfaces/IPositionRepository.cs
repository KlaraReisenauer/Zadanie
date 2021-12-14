using System.Collections.Generic;
using ZadanieAPI.Models;

namespace ZadanieAPI.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        IList<PositionDTO> GetAll();

        PositionDTO GetById(int id);

        PositionDTO Save(PositionDTO position);

        bool Remove(int id);
    }
}
