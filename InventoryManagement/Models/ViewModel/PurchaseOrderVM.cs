using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models.ViewModel
{
    public class PurchaseOrderVM
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public SelectList Suppliers { get; set; }
        public SelectList Warehouses { get; set; }
    }
}
