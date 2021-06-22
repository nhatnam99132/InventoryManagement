using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Unit : DatetimeEntity
    {
        public Unit()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }
       

        public virtual ICollection<Product> Products { get; set; }
    }
}
