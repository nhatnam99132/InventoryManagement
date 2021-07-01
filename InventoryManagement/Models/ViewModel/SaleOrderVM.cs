using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models.ViewModel
{
    public class SaleOrderVM
    {
        public SaleOrder SaleOrder { get; set; }
        public SelectList Customers { get; set; }
        public SelectList Warehouses { get; set; }
    }
}
