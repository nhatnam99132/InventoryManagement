using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class SaleOrderDetail : DatetimeEntity
    {
        public int? Soid { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public double? Vat { get; set; }
        public double? PriceAfterDiscount { get; set; }
        public double? TotalAmount { get; set; }


        public virtual Product Product { get; set; }
        public virtual SaleOrder So { get; set; }
    }
}
