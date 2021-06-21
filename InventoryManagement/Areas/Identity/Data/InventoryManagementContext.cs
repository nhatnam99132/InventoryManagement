using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
    public class InventoryManagementContext : IdentityDbContext<Employee>
    {
        public InventoryManagementContext(DbContextOptions<InventoryManagementContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<EmployeeWareHouse> EmployeeWareHouses { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<Role> Rolez { get; set; }
        public virtual DbSet<RoleFunction> RoleFunctions { get; set; }
        public virtual DbSet<SaleOrder> SaleOrders { get; set; }
        public virtual DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            builder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContactName).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Vat).HasColumnName("VAT");

                entity.Property(e => e.WareHouse).HasMaxLength(250);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AuditLog_Product");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_AuditLog_Supplier");
            });

            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            builder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerAddress).HasMaxLength(250);

                entity.Property(e => e.CustomerName).HasMaxLength(250);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            builder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeAddress).HasMaxLength(250);

                entity.Property(e => e.EmployeeName).HasMaxLength(250);
            });

            builder.Entity<EmployeeRole>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.EmployeeId).HasMaxLength(450);
                entity.ToTable("EmployeeRole");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeRole_Employee");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_EmployeeRole_Roles");
            });

            builder.Entity<EmployeeWareHouse>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.EmployeeId).HasMaxLength(450);

                entity.ToTable("EmployeeWareHouse");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeWareHouse_Employee");

                entity.HasOne(d => d.Warehouse)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_EmployeeWareHouse_Warehouse");
            });

            builder.Entity<Function>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FunctionName).HasMaxLength(250);
            });

            builder.Entity<Inventory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Inventory");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Inventory_Product");

                entity.HasOne(d => d.Warehouse)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_Inventory_Warehouse");
            });

            builder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Length).HasMaxLength(100);

                entity.Property(e => e.ProductName).HasMaxLength(250);

                entity.Property(e => e.Width).HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Product_Unit");
            });

            builder.Entity<PurchaseDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PurchaseDetail");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Poid).HasColumnName("POId");

                entity.Property(e => e.Vat).HasColumnName("VAT");

                entity.HasOne(d => d.Po)
                    .WithMany()
                    .HasForeignKey(d => d.Poid)
                    .HasConstraintName("FK_PurchaseDetail_PurchaseOrder");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PurchaseDetail_Product");
            });

            builder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("PurchaseOrder");

                entity.Property(e => e.ContactName).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Potype)
                    .HasMaxLength(50)
                    .HasColumnName("POType");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_PurchaseOrder_Supplier");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_PurchaseOrder_Warehouse");
            });

            builder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleName).HasMaxLength(250);
            });

            builder.Entity<RoleFunction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RoleFunction");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Function)
                    .WithMany()
                    .HasForeignKey(d => d.FunctionId)
                    .HasConstraintName("FK_RoleFunction_Functions");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleFunction_Roles");
            });

            builder.Entity<SaleOrder>(entity =>
            {
                entity.ToTable("SaleOrder");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sotype)
                    .HasMaxLength(50)
                    .HasColumnName("SOType");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SaleOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_SaleOrder_Customer");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.SaleOrders)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_SaleOrder_Warehouse");
            });

            builder.Entity<SaleOrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SaleOrderDetail");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Soid).HasColumnName("SOId");

                entity.Property(e => e.Vat).HasColumnName("VAT");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_SaleOrderDetail_Product");

                entity.HasOne(d => d.So)
                    .WithMany()
                    .HasForeignKey(d => d.Soid)
                    .HasConstraintName("FK_SaleOrderDetail_SaleOrder");
            });

            builder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerName).HasMaxLength(250);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierAddress).HasMaxLength(250);

                entity.Property(e => e.SupplierName).HasMaxLength(250);
            });

            builder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UnitName).HasMaxLength(250);
            });

            builder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerName).HasMaxLength(250);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseAddress).HasMaxLength(250);

                entity.Property(e => e.WarehouseName).HasMaxLength(250);
            });

           // OnModelCreatingPartial(builder);
        }

       // partial void OnModelCreatingPartial(ModelBuilder builder);
    }
}

