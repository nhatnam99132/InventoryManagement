using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Warehouse : DatetimeEntity
    {
        public Warehouse()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SaleOrders = new HashSet<SaleOrder>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Warehouse Name")]
        public string WarehouseName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string WarehouseAddress { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^([0-9]{9,11})$", ErrorMessage = "Invalid Phone Number. Please Try Again")]
   
        public string PhoneNumber { get; set; }
        

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
