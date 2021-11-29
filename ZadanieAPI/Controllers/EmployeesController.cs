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
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeRepository _employeeRepository; //TODO: add mappings

        public EmployeesController(ILogger<EmployeesController> logger,
            IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IEnumerable<EmployeeDTO> Get()
        {//TODO: Add contraints, how many records should I return for pagination (all, or)???
            return _employeeRepository.GetAll();
        }
    }
}
