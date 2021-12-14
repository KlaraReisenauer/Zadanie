using System;
using System.Collections.Generic;
using System.Linq;
using ZadanieAPI.Repositories.Interfaces;
using ZadanieAPI.Models;

namespace ZadanieAPI.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        // TODO: Remove after db is properly connected
        private IList<PositionDTO> _positions = new List<PositionDTO>(){
                new PositionDTO
                {
                    Id = 1,
                    Name = "Other"
                },
                new PositionDTO
                {
                    Id = 2,
                    Name = "Tester"
                },
                new PositionDTO
                {
                    Id = 3,
                    Name = "Programmer"
                },
                new PositionDTO
                {
                    Id = 4,
                    Name = "Support"
                },
                new PositionDTO
                {
                    Id = 5,
                    Name = "Analyst"
                },
                new PositionDTO
                {
                    Id = 6,
                    Name = "Tradesman"
                }
            };

        public IList<PositionDTO> GetAll()
        {
            return _positions;
        }

        public PositionDTO GetById(int id)
        {
            PositionDTO position = _positions.FirstOrDefault(p => p.Id == id);

            if (position == default(PositionDTO))
            { // should I throw exception or automatically add new?? I think throw exception and user can possibly add position again..
                throw new Exception("Position does not exist in current scope.");
            }

            return position;
        }

        public bool Remove(int id)
        {
            PositionDTO posToRemove = _positions.FirstOrDefault(p => p.Id == id);
            if (posToRemove != default(PositionDTO))
            {
                return _positions.Remove(posToRemove);
            }

            return true;
        }

        public PositionDTO Save(PositionDTO position)
        {
            if (position.Id <= 0)
            {
                return AddNewPosition(position);
            }

            return EditPosition(position);
        }

        private PositionDTO AddNewPosition(PositionDTO position)
        {
            position.Id = _positions.Max(p => p.Id) + 1;
            _positions.Add(position);
            return position;
        }

        private PositionDTO EditPosition(PositionDTO position)
        {
            PositionDTO posToUpdate = GetById(position.Id);
            posToUpdate.Name = position.Name; // this wont update list value..
            return position;
        }
    }
}
