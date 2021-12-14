using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ZadanieAPI.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IEnumerable<EmployeeDTO> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        [HttpGet("{id}")] //called as /api/[controller]/id
        public EmployeeDTO GetById(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("Employee id is empty");
            }

            return _employeeRepository.GetById(id);
        }

        [HttpPost]
        public string Save(EmployeeDTO employee) //TODO: Empty Guid => 00000000-0000-0000-0000-000000000000
        {
            if (employee == null || _employeeRepository.CheckEmployeeEmpty(employee))
            {
                throw new ArgumentException("Employee data is empty or it is not validly filled. Check required options.");
            }

            EmployeeDTO empl = _employeeRepository.Save(employee);
            return empl.Id.ToString();  //TODO: is it good practice??
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
    }
}
