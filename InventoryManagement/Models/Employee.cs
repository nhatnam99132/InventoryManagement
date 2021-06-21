using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class Employee : IdentityUser
    {
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

