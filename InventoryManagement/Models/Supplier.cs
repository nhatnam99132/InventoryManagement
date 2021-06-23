using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Supplier : DatetimeEntity
    {
        public Supplier()
        {
            AuditLogs = new HashSet<AuditLog>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Required]
        [Display(Name = "Supplier Address")]
        public string SupplierAddress { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^([0-9]{9,11})$", ErrorMessage = "Invalid Phone Number. Please Try Again")]
        public string PhoneNumber { get; set; }
        

        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
