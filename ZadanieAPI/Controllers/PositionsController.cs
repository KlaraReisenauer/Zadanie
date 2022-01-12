using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var positions = _positionRepository.GetAll();
            return Ok(positions.Select(p => _mapper.Map<PositionDTO>(p)));
        }

        [HttpPost]
        public IActionResult Save(PositionDTO position)
        {
            if (string.IsNullOrEmpty(position.Name) ||
                string.IsNullOrWhiteSpace(position.Name))
            {
                return BadRequest("Position is not in valid format. Position name cannot be empty.");
            }

            return Ok(_positionRepository.Save(
                _mapper.Map<Position>(position)));
        }

        [HttpDelete("{positionId}")]
        public IActionResult Remove(int positionId)
        {
            if (positionId <= 0)
            {
                return BadRequest("Position id format is not valid.");
            }

            _positionRepository.Remove(positionId);
            return Ok();
        }
    }
}
