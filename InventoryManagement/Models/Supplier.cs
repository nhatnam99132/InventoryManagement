using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            AuditLogs = new HashSet<AuditLog>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
