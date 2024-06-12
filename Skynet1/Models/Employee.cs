using System;
using System.Collections.Generic;

namespace Skynet1.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string? NameEmployee { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? RolId { get; set; }
    }
}
