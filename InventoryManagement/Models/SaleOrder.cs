using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class SaleOrder : DatetimeEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Warehouse Name")]
        public int? WarehouseId { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public int? CustomerId { get; set; }
        [Required]
        [Display(Name = "Sale Order Type")]
        public string Sotype { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
