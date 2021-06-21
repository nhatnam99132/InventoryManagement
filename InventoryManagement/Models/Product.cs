﻿using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManagement.Models
{
    public partial class Product
    {
        public Product()
        {
            AuditLogs = new HashSet<AuditLog>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? UnitId { get; set; }
        public int? CategoryId { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
}