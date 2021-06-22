using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Function : DatetimeEntity
    {
        public int Id { get; set; }
        public string FunctionName { get; set; }

    }
}
