using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ZadanieAPI.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PastEmployeesController : ControllerBase
    {
        private readonly IPastEmployeeRepository _pastEmployeeRepository;
        private readonly IMapper _mapper;

        public PastEmployeesController(IPastEmployeeRepository pastEmployee,
            IMapper mapper)
        {
            _pastEmployeeRepository = pastEmployee;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pastEmployees = _pastEmployeeRepository.GetAll();
            return Ok(pastEmployees.Select(p => _mapper.Map<PastEmployeeDTO>(p)));
        }
        
        [HttpDelete("{employeeId}")]
        public IActionResult Remove(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return BadRequest("Past employee id is empty");
            }
            
            _pastEmployeeRepository.Remove(employeeId);
            return Ok();
        }
    }
}
