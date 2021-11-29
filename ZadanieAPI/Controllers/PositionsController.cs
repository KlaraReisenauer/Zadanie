using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieAPI.Data;
using ZadanieAPI.Data.Repositories;

namespace ZadanieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] //Positions
    public class PositionsController : ControllerBase
    {
        private readonly ILogger<PositionsController> _logger;
        private readonly IPositionsRepository _positionsRepository;

        public PositionsController(ILogger<PositionsController> logger,
            IPositionsRepository positionsRepository)
        {
            _logger = logger;
            _positionsRepository = positionsRepository;
        }

        [HttpGet]
        public IEnumerable<PositionDTO> Get()
        {
            return _positionsRepository.GetAll();
        }

    }
}
