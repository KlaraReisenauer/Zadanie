using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ZadanieAPI.Database.Models
{
    [Index(nameof(Name), Name = "UQ__Position__737584F6B02BFBC6", IsUnique = true)]
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int PositionId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RemovedOn { get; set; }

        [InverseProperty(nameof(Employee.Position))]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
