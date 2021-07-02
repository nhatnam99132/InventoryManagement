using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models.ViewModel
{
    public class EmployeeWarehouseVM
    {
        public EmployeeWareHouse EmployeeWareHouse { get; set; }
        public SelectList Warehouses { get; set; }
        public SelectList Employees { get; set; }
    }
}
