using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ZadanieAPI.Database.Models;
using ZadanieAPI.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionsController(IPositionRepository positionRepository,
            IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<PositionDTO> GetAll()
        {
            var positions = _positionRepository.GetAll();
            return positions.Select(p => _mapper.Map<PositionDTO>(p));
        }

        [HttpGet("{id}")] //called as /api/[controller]/id
        public PositionDTO GetById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Position id format is not valid");
            }

            var position = _positionRepository.GetById(id);
            return _mapper.Map<PositionDTO>(position);
        }

        [HttpPost]
        public int Save(PositionDTO position)
        {
            if(string.IsNullOrEmpty(position.Name) ||
                string.IsNullOrWhiteSpace(position.Name))
            {
                throw new ArgumentException("Position is not in valid format. Position name cannot be empty.");
            }

            var savedPosition = _positionRepository.Save(
                _mapper.Map<Position>(position));
            return savedPosition.PositionId;
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
