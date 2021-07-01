using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Inventory
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Warehouse Name")]
        public int? WarehouseId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public int? ProductId { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
