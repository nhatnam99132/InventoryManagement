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
    public class SaleOrderController : Controller
    {
        private readonly InventoryManagementContext _context;

        public SaleOrderController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: SaleOrders
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var saleorders = from m in _context.SaleOrders
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        saleorders = saleorders.Where(s => s.CustomerName.Contains(searchString));
        //    }

        //    return View(await saleorders.ToListAsync());
        //    //return View(await _context.SaleOrders.ToListAsync());
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

            var saleorders = from s in _context.SaleOrders.Include(d => d.Warehouse).Include(d => d.Customer).AsNoTracking()
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                saleorders = saleorders.Where(s => s.Customer.CustomerName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    saleorders = saleorders.OrderByDescending(s => s.Customer.CustomerName);
                    break;
                case "Date":
                    saleorders = saleorders.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    saleorders = saleorders.OrderByDescending(s => s.CreatedDate);
                    break;

                default:
                    saleorders = saleorders.OrderBy(s => s.Customer.CustomerName);
                    break;
            }

            int pageSize = 10;



            return View(await PaginatedListSaleOrder<SaleOrder>.CreateAsync(saleorders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: SaleOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.SaleOrders
                .Include(d => d.Warehouse).Include(d => d.Customer).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: SaleOrders/Create
        public async Task<IActionResult> Create()
        {
            var customer = await _context.Customers.OrderBy(t => t.CustomerName).ToListAsync();
            var warehouse = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            var saleOrderVM = new SaleOrderVM
            {
                Customers = new SelectList(customer, "Id", "CustomerName"),
                Warehouses = new SelectList(warehouse, "Id", "WarehouseName"),
            };
            return View(saleOrderVM);
        }

        // POST: SaleOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WarehouseId,CustomerId,Sotype,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] SaleOrder saleOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleOrder);
        }

        // GET: SaleOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.SaleOrders.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.OrderBy(t => t.CustomerName).ToListAsync();
            var warehouse = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            var saleOrderVM = new SaleOrderVM
            {
                Customers = new SelectList(customer, "Id", "CustomerName"),
                Warehouses = new SelectList(warehouse, "Id", "WarehouseName"),
                SaleOrder = unit
            };
            return View(saleOrderVM);
        }

        // POST: SaleOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WarehouseId,CustomerId,Sotype,CreatedDate,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] SaleOrder saleOrder)
        {
            if (id != saleOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!unitExists(saleOrder.Id))
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
            return View(saleOrder);
        }

        // GET: SaleOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.SaleOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: SaleOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.SaleOrders.FindAsync(id);
            _context.SaleOrders.Remove(unit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool unitExists(int id)
        {
            return _context.SaleOrders.Any(e => e.Id == id);
        }
    }
}
