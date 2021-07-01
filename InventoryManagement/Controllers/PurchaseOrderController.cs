using InventoryManagement.Data;
using InventoryManagement.Models;
using InventoryManagement.Models.ViewModel;
using InventoryManagement.PaginatedList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly InventoryManagementContext _context;

        public PurchaseOrderController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrders
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var purchaseorders = from m in _context.PurchaseOrders
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        purchaseorders = purchaseorders.Where(s => s.CustomerName.Contains(searchString));
        //    }

        //    return View(await purchaseorders.ToListAsync());
        //    //return View(await _context.PurchaseOrders.ToListAsync());
        //}
        public async Task<IActionResult> Index(
     string sortOrder,
     string searchString,
     int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }

            ViewData["CurrentFilter"] = searchString;

            var purchaseorders = from s in _context.PurchaseOrders.Include(d => d.Warehouse).Include(d => d.Supplier).AsNoTracking()
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                purchaseorders = purchaseorders.Where(s => s.Supplier.SupplierName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    purchaseorders = purchaseorders.OrderByDescending(s => s.Supplier.SupplierName);
                    break;
                case "Date":
                    purchaseorders = purchaseorders.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    purchaseorders = purchaseorders.OrderByDescending(s => s.CreatedDate);
                    break;

                default:
                    purchaseorders = purchaseorders.OrderBy(s => s.Supplier.SupplierName);
                    break;
            }

            int pageSize = 10;



            return View(await PaginatedListPurchaseOrder<PurchaseOrder>.CreateAsync(purchaseorders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: PurchaseOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseorder = await _context.PurchaseOrders
                .Include(d => d.Warehouse).Include(d => d.Supplier).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseorder == null)
            {
                return NotFound();
            }

            return View(purchaseorder);
        }

        // GET: PurchaseOrders/Create
        public async Task<IActionResult> Create()
        {
            var suppliers = await _context.Suppliers.OrderBy(t => t.SupplierName).ToListAsync();
            var warehouse = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            var purchaseOrderVM = new PurchaseOrderVM
            {
                Suppliers = new SelectList(suppliers, "Id", "SupplierName"),
                Warehouses = new SelectList(warehouse, "Id", "WarehouseName"),
            };
            return View(purchaseOrderVM);
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WarehouseId,SupplierId,Potype,ContactName,PhoneNumber,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseorder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseorder == null)
            {
                return NotFound();
            }
            var suppliers = await _context.Suppliers.OrderBy(t => t.SupplierName).ToListAsync();
            var warehouse = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            var purchaseOrderVM = new PurchaseOrderVM
            {
                Suppliers = new SelectList(suppliers, "Id", "CustomerName"),
                Warehouses = new SelectList(warehouse, "Id", "WarehouseName"),
                PurchaseOrder = purchaseorder
            };
            return View(purchaseOrderVM);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WarehouseId,SupplierId,Potype,ContactName,PhoneNumber,CreatedDate,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!purchaseorderExists(purchaseOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseorder = await _context.PurchaseOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseorder == null)
            {
                return NotFound();
            }

            return View(purchaseorder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseorder = await _context.PurchaseOrders.FindAsync(id);
            _context.PurchaseOrders.Remove(purchaseorder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool purchaseorderExists(int id)
        {
            return _context.PurchaseOrders.Any(e => e.Id == id);
        }
    }
}
