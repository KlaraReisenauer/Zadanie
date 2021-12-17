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

        //[HttpGet("{id}")] //called as /api/[controller]/id
        //public EmployeeDTO GetById(Guid id)
        //{
        //    if(id == Guid.Empty)
        //    {
        //        throw new ArgumentException("Employee id is empty");
        //    }
        //    Employee employee = _employeeRepository.GetById(id);
        //    return _mapper.Map<EmployeeDTO>(employee);
        //}

        [HttpPost]
        public string Save(EmployeeDTO employee)
        {
            if (employee == null || CheckEmployeeEmpty(employee))
            {
                throw new ArgumentException("Employee data is empty or it is not validly filled. Check required options.");
            }

            Employee empl = _employeeRepository.Save(
                _mapper.Map<Employee>(employee));
            return empl.EmployeeId.ToString();  //TODO: is it good practice??
        }

        [HttpDelete]
        public void Remove(Guid employeeId, bool removePermanently = false)
        {
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentException("Employee id is empty");
            }

            _employeeRepository.Remove(employeeId, removePermanently);
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
