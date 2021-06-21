using InventoryManagement.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class EmployeeWareHouse
    {
        public string? EmployeeId { get; set; }
        public int? WarehouseId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
