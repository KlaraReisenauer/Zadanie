using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ZadanieAPI.Models;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PastEmployeesController : ControllerBase
    {
        private readonly IPastEmployeeRepository _pastEmployee;

        public PastEmployeesController(IPastEmployeeRepository pastEmployee)
        {
            _pastEmployee = pastEmployee;
        }

        [HttpGet]
        public IEnumerable<PastEmployeeDTO> GetAll()
        {
            return _pastEmployee.GetAll();
        }

        [HttpGet("{id}")] //called as /api/[controller]/id TODO: hide path on client?
        public PastEmployeeDTO GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Past employee id is empty");
            }

            return _pastEmployee.GetById(id);
        }

        [HttpDelete("{id}")]
        public void Remove(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Past employee id is empty");
            }

            _pastEmployee.Remove(id);
        }
    }
}
