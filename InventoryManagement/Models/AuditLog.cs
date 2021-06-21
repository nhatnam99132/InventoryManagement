using System;


namespace InventoryManagement.Models
{
    public partial class AuditLog
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string WareHouse { get; set; }
        public int? SupplierId { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public double? Vat { get; set; }
        public double? PriceAfterDiscount { get; set; }
        public double? TotalAmount { get; set; }
        public int? CustomerId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
