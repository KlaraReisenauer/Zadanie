using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZadanieAPI.Data.DTOs;
using ZadanieAPI.Data.Repositories.Interfaces;

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
        public IEnumerable<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        [HttpGet("{id}")] //called as /api/[controller]/id
        public Employee GetById(string id)
        {
            if(string.IsNullOrEmpty(id) && 
                string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Employee id is empty");
            }

            return _employeeRepository.GetById(new Guid(id));
        }

        [HttpPost]
        public string Save(Employee employee) //TODO: Empty Guid => 00000000-0000-0000-0000-000000000000
        {
            if (employee == null && EmployeeEmpty(employee))
            {
                throw new ArgumentException("Employee data is empty or it is not validly filled. Check required options.");
            }

            Employee empl = _employeeRepository.Save(employee);
            return empl.Id.ToString();  //TODO: is it good practice??
        }

        [HttpDelete]
        public void Remove(string employeeId, bool removePermanently = false)
        {
            if (string.IsNullOrEmpty(employeeId) ||
                string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentException("Employee id is empty");
            }

            _employeeRepository.Remove(new Guid(employeeId), removePermanently);
        }

        private bool EmployeeEmpty(Employee employee)
        {
            return string.IsNullOrEmpty(employee.Name) &&
                string.IsNullOrEmpty(employee.Surname) &&
                employee.PositionId == -1 &&
                employee.Salary == 0 &&
                employee.DateOfBirth == default(DateTime);
        }
    }
}
