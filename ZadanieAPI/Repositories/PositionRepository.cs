using System;
using System.Collections.Generic;
using System.Linq;
using ZadanieAPI.Repositories.Interfaces;
using ZadanieAPI.Models;
using ZadanieAPI.Database.Models;

namespace ZadanieAPI.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        // TODO: Remove after db is properly connected
        private IList<Position> _positions = new List<Position>(){
                new Position
                {
                    PositionId = 1,
                    Name = "Other"
                },
                new Position
                {
                    PositionId = 2,
                    Name = "Tester"
                },
                new Position
                {
                    PositionId = 3,
                    Name = "Programmer"
                },
                new Position
                {
                    PositionId = 4,
                    Name = "Support"
                },
                new Position
                {
                    PositionId = 5,
                    Name = "Analyst"
                },
                new Position
                {
                    PositionId = 6,
                    Name = "Tradesman"
                }
            };
        private readonly CoreDbContext _dbContext;

        public PositionRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Public Methods

        public IList<Position> GetAll()
        {
            var positions = _dbContext.Positions.ToList();
            //TODO: remove when above working
            return _positions;
        }

        public bool Remove(int id)
        {
            try
            {
                var positionToRemove = GetById(id);
                _dbContext.Positions.Remove(positionToRemove);
                _dbContext.SaveChanges();
            }
            catch (Exception) { } //if already removed - no problem

            return true;

            //TODO: remove when above working
            Position posToRemove = _positions.FirstOrDefault(p => p.PositionId == id);
            if (posToRemove != default(Position))
            {
                return _positions.Remove(posToRemove);
            }

            return true;
        }

        public Position Save(Position position)
        {
            if (position.PositionId <= 0)
            {
                return AddNewPosition(position);
            }

            return EditPosition(position);
        }

        #endregion Public Methods

        #region Private Methods

        private Position GetById(int id)
        {
            var position = _dbContext.Positions
                .FirstOrDefault(p => p.PositionId == id);
            return position == default(Position) ?
                throw new Exception("Position does not exist in current scope.")
                : position;
        }

        private Position AddNewPosition(Position position)
        {
            var newPosition = _dbContext.Positions.Add(
                new Position {
                    Name = position.Name
                });
            _dbContext.SaveChanges();

            //TODO: remove when above working
            position.PositionId = _positions.Max(p => p.PositionId) + 1;
            _positions.Add(position);
            return position;
        }

        private Position EditPosition(Position position)
        {
            var positionToUpdate = GetById(position.PositionId);
            var result = _dbContext.Positions.Update(positionToUpdate);
            _dbContext.SaveChanges();

            //TODO: remove when above working
            Position posToUpdate = _positions.First(p => p.PositionId == position.PositionId);
            posToUpdate.Name = position.Name; 
            return position;
        }

        #endregion Private Methods
    }
}
