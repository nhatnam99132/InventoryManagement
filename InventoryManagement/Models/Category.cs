using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Category : DatetimeEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
     
        public virtual ICollection<Product> Products { get; set; }
    }
}
