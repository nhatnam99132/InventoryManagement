using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Role : DatetimeEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
       
    }
}
