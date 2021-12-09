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
    public class PastEmployeesController : ControllerBase
    {
        private readonly IPastEmployeeRepository _pastEmployee;

        public PastEmployeesController(IPastEmployeeRepository pastEmployee)
        {
            _pastEmployee = pastEmployee;
        }

        [HttpGet]
        public IEnumerable<PastEmployee> GetAll()
        {
            return _pastEmployee.GetAll();
        }

        [HttpGet("{id}")] //called as /api/[controller]/id TODO: hide path on client?
        public PastEmployee GetById(string id)
        {
            if (string.IsNullOrEmpty(id) ||
                   string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Past employee id is empty");
            }

            return _pastEmployee.GetById(new Guid(id));
        }

        [HttpDelete("{id}")]
        public void Remove(string id)
        {
            if (string.IsNullOrEmpty(id) ||
                string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Past employee id is empty");
            }

            _pastEmployee.Remove(new Guid(id));
        }
    }
}
