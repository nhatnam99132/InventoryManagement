using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class DatetimeEntity
    {
        [Display(Name = "Create Date")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Update Date")]
        public DateTime? UpdatedDate { get; set; }
        [Display(Name = "Create By")]
        public int? CreatedBy { get; set; }
        [Display(Name = "Update By")]
        public int? UpdatedBy { get; set; }
    }
}
