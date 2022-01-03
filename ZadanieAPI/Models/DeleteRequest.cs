using System;

namespace ZadanieAPI.Models
{
    public class DeleteRequest
    {
        public Guid EmployeeId {get; set;}

        public bool RemovePermanently {get; set;} = false;
    }
}