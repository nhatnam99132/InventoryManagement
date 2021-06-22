using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class EmployeeRole : DatetimeEntity
    {
        public string? EmployeeId { get; set; }
        public int? RoleId { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
