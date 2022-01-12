using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ZadanieAPI.Database.Models;
using ZadanieAPI.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeRepository.GetAll();
            return Ok(employees.Select(e => _mapper.Map<EmployeeDTO>(e)).ToList());
        }

        [HttpPost]
        public IActionResult Save(EmployeeDTO employee)
        {
            if (employee == null || CheckEmployeeNotValid(employee))
            {
                return BadRequest("Employee data is empty or it is not validly filled. Check required options.");
            }

            Guid emplId = _employeeRepository.Save(
                _mapper.Map<Employee>(employee));
                
            return Ok(emplId);
        }

        [HttpDelete]
        public IActionResult Remove(DeleteRequest request)
        {
            if (request.EmployeeId == Guid.Empty)
            {
                return BadRequest("Employee id is empty");
            }

            _employeeRepository.Remove(request.EmployeeId, request.RemovePermanently);
            return Ok();
        }

        private static bool CheckEmployeeNotValid(EmployeeDTO employee)
        {
            return string.IsNullOrEmpty(employee.Name) ||
                string.IsNullOrWhiteSpace(employee.Name) ||
                string.IsNullOrEmpty(employee.Surname) ||
                string.IsNullOrWhiteSpace(employee.Surname) ||
                employee.PositionId <= 0 ||
                employee.Salary == 0 ||
                employee.DateOfBirth == default(DateTime);
        }

    }
}
