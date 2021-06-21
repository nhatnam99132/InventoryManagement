using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SaleOrders = new HashSet<SaleOrder>();
        }

        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
