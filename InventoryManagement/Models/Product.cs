using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Product : DatetimeEntity
    {
        public Product()
        {
            AuditLogs = new HashSet<AuditLog>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Unit Name")]
        public int? UnitId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public int? CategoryId { get; set; }
        [Required]
        [Display(Name = "Length")]
        public string Length { get; set; }
        [Required]
        [Display(Name = "Width")]
        public string Width { get; set; }
     

        public virtual Category Category { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
}
