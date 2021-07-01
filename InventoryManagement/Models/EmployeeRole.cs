using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class EmployeeRole : DatetimeEntity
    {
        public int Id { get; set; }
        public string? EmployeeId { get; set; }
        public int? RoleId { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
