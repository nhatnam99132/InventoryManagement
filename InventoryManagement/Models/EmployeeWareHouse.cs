using InventoryManagement.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class EmployeeWareHouse : DatetimeEntity
    {
        public string? EmployeeId { get; set; }
        public int? WarehouseId { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
