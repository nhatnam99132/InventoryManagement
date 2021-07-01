using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class RoleFunction : DatetimeEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? FunctionId { get; set; }
 

        public virtual Function Function { get; set; }
        public virtual Role Role { get; set; }
    }
}
