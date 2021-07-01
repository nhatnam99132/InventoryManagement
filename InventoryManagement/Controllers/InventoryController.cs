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
    public class InventoryController : Controller
    {
        private readonly InventoryManagementContext _context;

        public InventoryController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: Inventories
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var inventories = from m in _context.Inventories
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        inventories = inventories.Where(s => s.ProductName.Contains(searchString));
        //    }

        //    return View(await inventories.ToListAsync());
        //    //return View(await _context.Inventories.ToListAsync());
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

            var inventories = from s in _context.Inventories.Include(d => d.Warehouse).Include(d => d.Product).ThenInclude(d => d.Category).AsNoTracking()
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                inventories = inventories.Where(s => s.Product.ProductName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    inventories = inventories.OrderByDescending(s => s.Product.ProductName);
                    break;
                case "Date":
                    inventories = inventories.OrderBy(s => s.Quantity);
                    break;
                case "date_desc":
                    inventories = inventories.OrderByDescending(s => s.Quantity);
                    break;

                default:
                    inventories = inventories.OrderBy(s => s.Product.ProductName);
                    break;
            }

            int pageSize = 10;



            return View(await PageListInventory<Inventory>.CreateAsync(inventories.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Inventories
                .Include(d => d.Warehouse).Include(d => d.Product).ThenInclude(d => d.Category).AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Inventories/Create
        public async Task<IActionResult> Create()
        {
            var products = await _context.Products.OrderBy(t => t.ProductName).ToListAsync();
            var warehouses = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            var inventoryVM = new InventoryVM
            {
                Products = new SelectList(products, "Id", "ProductName"),
                Warehouses = new SelectList(warehouses, "Id", "WarehouseName")
            };
            return View(inventoryVM);
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,WarehouseId,Quantity")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Inventories.Include(d => d.Warehouse).Include(d => d.Product).ThenInclude(d => d.Category).FirstOrDefaultAsync(m => m.ProductId == id);
            if (unit == null)
            {
                return NotFound();
            }
            var products = await _context.Products.OrderBy(t => t.ProductName).ToListAsync();
            var warehouses = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            var inventoryVM = new InventoryVM
            {
                Products = new SelectList(products, "Id", "ProductName"),
                Warehouses = new SelectList(warehouses, "Id", "WarehouseName"),
                Inventory = unit
            };
            return View(inventoryVM);
        }

        //POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,WarehouseId,Quantity")] Inventory inventory)
        {
            if (id != inventory.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!unitExists(inventory.ProductId))
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
            return View(inventory);
        }

        //// GET: Inventories/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var unit = await _context.Inventories.Include(d => d.Warehouse).Include(d => d.Product).ThenInclude(d => d.Category)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (unit == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(unit);
        // }

        // // POST: Inventories/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var unit = await _context.Inventories.Include(d => d.Warehouse).Include(d => d.Product).ThenInclude(d => d.Category).FindAsync(id);
        //     _context.Inventories.Remove(unit);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        private bool unitExists(int? id)
        {
            return _context.Inventories.Any(e => e.ProductId == id);
        }
    }
}
