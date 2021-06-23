using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class SupplierController : Controller
    {
        private readonly InventoryManagementContext _context;

        public SupplierController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var suppliers = from m in _context.Suppliers
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        suppliers = suppliers.Where(s => s.SupplierName.Contains(searchString));
        //    }

        //    return View(await suppliers.ToListAsync());
        //    //return View(await _context.Suppliers.ToListAsync());
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

            var suppliers = from s in _context.Suppliers
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                suppliers = suppliers.Where(s => s.SupplierName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    suppliers = suppliers.OrderByDescending(s => s.SupplierName);
                    break;
                case "Date":
                    suppliers = suppliers.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    suppliers = suppliers.OrderByDescending(s => s.CreatedDate);
                    break;

                default:
                    suppliers = suppliers.OrderBy(s => s.SupplierName);
                    break;
            }

            int pageSize = 10;



            return View(await PaginatedList1<Supplier>.CreateAsync(suppliers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SupplierName,SupplierAddress,CustomerName,PhoneNumber,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Suppliers.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SupplierName,SupplierAddress,CustomerName,PhoneNumber,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!supplierExists(supplier.Id))
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
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(unit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool supplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
