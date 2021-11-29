using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieAPI.Data.Repositories
{
    public interface IPositionsRepository
    {
        /// <summary>
        /// Retrieve all actual positions from database
        /// </summary>
        /// <returns>List of positions</returns>
        public IList<PositionDTO> GetAll();

        /// <summary>
        /// Retrieve single position from database based on given id
        /// </summary>
        /// <param name="positionId">ID identifying wanted position</param>
        /// <returns>Position data</returns>
        public PositionDTO GetById(Guid positionId);

        /// <summary>
        /// Save changes to existing position
        /// </summary>
        /// <param name="position">position data</param>
        public void Save(EmployeeDTO position);

        /// <summary>
        /// Set RemovedOn datetime stamp on given position
        /// </summary>
        /// <param name="positionId">Id identifying position, which is to be removed</param>
        public void Remove(Guid positionId);

        /// <summary>
        /// Add new position to database
        /// </summary>
        /// <param name="position">New position data</param>
        public void Add(PositionDTO position);
    }
}
