using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models.ViewModel
{
    public class SaleOrderDetailVM
    {
        public SaleOrderDetail SaleOrderDetail { get; set; }
        public SelectList Products { get; set; }
        public SelectList SaleOrders { get; set; }
    }
}
