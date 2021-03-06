using InventoryManagement.Data;
using InventoryManagement.Models;
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
    public class WarehouseController : Controller
    {
        private readonly InventoryManagementContext _context;

        public WarehouseController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: Warehouses
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var warehouses = from m in _context.Warehouses
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        warehouses = warehouses.Where(s => s.WarehouseName.Contains(searchString));
        //    }

        //    return View(await warehouses.ToListAsync());
        //    //return View(await _context.Warehouses.ToListAsync());
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

            var warehouses = from s in _context.Warehouses
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                warehouses = warehouses.Where(s => s.WarehouseName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    warehouses = warehouses.OrderByDescending(s => s.WarehouseName);
                    break;
                case "Date":
                    warehouses = warehouses.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    warehouses = warehouses.OrderByDescending(s => s.CreatedDate);
                    break;

                default:
                    warehouses = warehouses.OrderBy(s => s.WarehouseName);
                    break;
            }
            
            int pageSize = 10;
            
         

            return View(await PaginatedList1<Warehouse>.CreateAsync(warehouses.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Warehouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Warehouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WarehouseName,WarehouseAddress,PhoneNumber,CustomerName,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Warehouses.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WarehouseName,WarehouseAddress,PhoneNumber,CustomerName,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!unitExists(warehouse.Id))
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
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Warehouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.Warehouses.FindAsync(id);
            _context.Warehouses.Remove(unit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool unitExists(int id)
        {
            return _context.Warehouses.Any(e => e.Id == id);
        }
    }
}