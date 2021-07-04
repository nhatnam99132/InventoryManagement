using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class SaleOrderDetail : DatetimeEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sale Order Id")]
        public int? Soid { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public int? ProductId { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Quantity { get; set; }
        [Required]
        [Display(Name = "Price")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Price Number")]
        [DataType(DataType.Currency)]
   
        public double? Price { get; set; }

        [Required]
        [Display(Name = "Discount")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid discount Number")]
        public double? Discount { get; set; }

        [Required]
        [Display(Name = "Vat")]
        [Range(0, 100, ErrorMessage = "Please enter valid Vat Number")]
        public double? Vat { get; set; }

        [Required]
        [Display(Name = "Price After Discount")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid decimal Number")]
        public double? PriceAfterDiscount { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid decimal Number")]
        public double? TotalAmount { get; set; }


        public virtual Product Product { get; set; }
        public virtual SaleOrder So { get; set; }
    }
}
