using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models.ViewModel
{
    public class ProductCategoryViewModel
    {
        public Product Product { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Units { get; set; }

    }
}
