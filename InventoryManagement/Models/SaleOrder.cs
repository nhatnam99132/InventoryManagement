using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class SaleOrder
    {
        public int Id { get; set; }
        public int? WarehouseId { get; set; }
        public int? CustomerId { get; set; }
        public string Sotype { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
