using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieAPI.Data.DTOs
{
    public class Employee
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PositionId { get; set; }
        public double Salary { get; set; }
        public DateTime StartDate { get; set; }
    }
}
