using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ZadanieAPI.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionRepository _positionRepository;

        public PositionsController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        [HttpGet]
        public IEnumerable<PositionDTO> GetAll()
        {
            return _positionRepository.GetAll();
        }

        [HttpGet("{id}")] //called as /api/[controller]/id
        public PositionDTO GetById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Position id format is not valid");
            }

            return _positionRepository.GetById(id);
        }

        [HttpPost]
        public PositionDTO Save(PositionDTO position)
        {
            if(string.IsNullOrEmpty(position.Name) ||
                string.IsNullOrWhiteSpace(position.Name))
            {
                throw new ArgumentException("Position is not in valid format. Position name cannot be empty.");
            }

            return _positionRepository.Save(position);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Position id format is not valid");
            }

            _positionRepository.Remove(id);
        }
    }
}
