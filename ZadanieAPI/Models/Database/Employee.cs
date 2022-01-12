using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ZadanieAPI.Database.Models
{
    public partial class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        public int PositionId { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Salary { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(PositionId))]
        [InverseProperty("Employees")]
        public virtual Position Position { get; set; }
    }
}
