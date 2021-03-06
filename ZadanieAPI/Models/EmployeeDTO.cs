using System;

namespace ZadanieAPI.Models
{
    public class EmployeeDTO
    {
        public Guid? EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PositionId { get; set; }
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; }
    }
}
