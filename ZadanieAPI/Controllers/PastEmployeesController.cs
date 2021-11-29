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
    [Route("[controller]")] //PastEmployees
    public class PastEmployeesController : ControllerBase
    {
        private readonly ILogger<PastEmployeesController> _logger;
        private readonly IPastEmployeeRepository _pastEmployeeRepository;

        public PastEmployeesController(ILogger<PastEmployeesController> logger,
            IPastEmployeeRepository pastEmployeeRepository)
        {
            _logger = logger;
            _pastEmployeeRepository = pastEmployeeRepository;
        }

        [HttpGet]
        public IEnumerable<PastEmployeeDTO> Get()
        {
            //try
            //{
                return _pastEmployeeRepository.GetAll();
            //}
            //catch(Exception Ex)
            //{
            //    _logger.LogError(Ex.Message);
            //    return null;
            //}
        }
    }
}
