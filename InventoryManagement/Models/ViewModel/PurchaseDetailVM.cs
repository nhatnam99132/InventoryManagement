using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models.ViewModel
{
    public class PurchaseDetailVM
    {
        public PurchaseDetail PurchaseDetail { get; set; }
        public SelectList Products { get; set; }
        public SelectList PurchaseOrders { get; set; }
        public Inventory Inventory { get; set; }
    }
}
