using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class EmployeeWareHouse : DatetimeEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Employee Name")]
        public string? EmployeeId { get; set; }
        [Required]
        [Display(Name = "Warehouse Name")]
        public int? WarehouseId { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
