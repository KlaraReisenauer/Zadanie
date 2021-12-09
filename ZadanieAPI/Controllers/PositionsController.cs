using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZadanieAPI.Data.DTOs;
using ZadanieAPI.Data.Repositories.Interfaces;

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
        public IEnumerable<Position> GetAll()
        {
            return _positionRepository.GetAll();
        }

        [HttpGet("{id}")] //called as /api/[controller]/id
        public Position GetById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Position id format is not valid");
            }

            return _positionRepository.GetById(id);
        }

        [HttpPost]
        public Position Save(Position position)
        {
            if(string.IsNullOrEmpty(position.Name) ||
                string.IsNullOrWhiteSpace(position.Name))
            {
                throw new ArgumentException("Position is not in valid format. Position name cannot be empty.");
            }

            return _positionRepository.Save(position);
        }

        [HttpDelete]
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
