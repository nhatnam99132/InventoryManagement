using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Customer : DatetimeEntity
    {
        public Customer()
        {
            SaleOrders = new HashSet<SaleOrder>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string CustomerAddress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^([0-9]{9,11})$", ErrorMessage = "Invalid Phone Number. Please Try Again")]
        public string PhoneNumber { get; set; }



        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
