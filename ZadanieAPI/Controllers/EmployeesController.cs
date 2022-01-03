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
        public IEnumerable<EmployeeDTO> GetAll()
        {
            IList<Employee> employees = _employeeRepository.GetAll();
            return employees.Select(e => _mapper.Map<EmployeeDTO>(e)).ToList();
        }

        [HttpPost]
        public Guid Save(EmployeeDTO employee)
        {
            if (employee == null || CheckEmployeeEmpty(employee))
            {
                throw new ArgumentException("Employee data is empty or it is not validly filled. Check required options.");
            }

            Guid emplId = _employeeRepository.Save(
                _mapper.Map<Employee>(employee));
                
            return emplId;
        }

        [HttpDelete]
        public void Remove(DeleteRequest request)
        {
            if (request.EmployeeId == Guid.Empty)
            {
                throw new ArgumentException("Employee id is empty");
            }

            _employeeRepository.Remove(request.EmployeeId, request.RemovePermanently);
        }

        private bool CheckEmployeeEmpty(EmployeeDTO employee)
        {
            return string.IsNullOrEmpty(employee.Name) &&
                string.IsNullOrEmpty(employee.Surname) &&
                employee.PositionId <= 0 &&
                employee.Salary == 0 &&
                employee.DateOfBirth == default(DateTime);
        }

    }
}
