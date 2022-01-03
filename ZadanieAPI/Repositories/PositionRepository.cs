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
        private readonly CoreDbContext _dbContext;

        public PositionRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Public Methods

        public IEnumerable<Position> GetAll()
        {
            return _dbContext.Positions.OrderBy(p => p.PositionId);
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
        }

        public int Save(Position position)
        {
            if (position.PositionId <= 0)
            {
                return AddNewPosition(position.Name);
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

        private int AddNewPosition(string positionName)
        {
            var newPosition = _dbContext.Positions.Add(
                new Position {
                    Name = positionName
                });
            _dbContext.SaveChanges();
            
            return newPosition.Entity.PositionId;
        }

        private int EditPosition(Position position)
        {
            if(GetById(position.PositionId) != null){
                var result = _dbContext.Positions.Update(position);
                _dbContext.SaveChanges();

                return result.Entity.PositionId;
            }

            throw new Exception("Position cannot be updated as it does not exist in current scope.");            
        }

        #endregion Private Methods
    }
}
