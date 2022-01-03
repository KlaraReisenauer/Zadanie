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
        public IEnumerable<PastEmployeeDTO> GetAll()
        {
            IList<Employee> pastEmployees = _pastEmployeeRepository.GetAll();
            return pastEmployees.Select(p => _mapper.Map<PastEmployeeDTO>(p));
        }
        
        [HttpDelete("{employeeId}")]
        public void Remove(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentException("Past employee id is empty");
            }
            _pastEmployeeRepository.Remove(employeeId);
        }
    }
}
