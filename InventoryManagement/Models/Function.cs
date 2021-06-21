using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Function
    {
        public int Id { get; set; }
        public string FunctionName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
