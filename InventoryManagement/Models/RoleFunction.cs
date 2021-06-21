using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class RoleFunction
    {
        public int RoleId { get; set; }
        public int? FunctionId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Function Function { get; set; }
        public virtual Role Role { get; set; }
    }
}
