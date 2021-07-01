using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class PurchaseOrder : DatetimeEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Warehouse Name")]
        public int? WarehouseId { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        public int? SupplierId { get; set; }
        [Required]
        [Display(Name = "Purchase Type")]
        public string Potype { get; set; }
        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^([0-9]{9,11})$", ErrorMessage = "Invalid Phone Number. Please Try Again")]
        public string PhoneNumber { get; set; }
      

        public virtual Supplier Supplier { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
