using System;
using System.Collections.Generic;

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
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNumber { get; set; }



        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
