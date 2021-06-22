using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class PurchaseDetail : DatetimeEntity
    {
        public int? Poid { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public double? Vat { get; set; }
        public double? PriceAfterDiscount { get; set; }
        public double? TotalAmount { get; set; }

        public virtual PurchaseOrder Po { get; set; }
        public virtual Product Product { get; set; }
    }
}
