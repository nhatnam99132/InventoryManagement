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
    public class CustomerController : Controller
    {
        private readonly InventoryManagementContext _context;

        public CustomerController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: Customers
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var customers = from m in _context.Customers
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        customers = customers.Where(s => s.CustomerName.Contains(searchString));
        //    }

        //    return View(await customers.ToListAsync());
        //    //return View(await _context.Customers.ToListAsync());
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

            var customers = from s in _context.Customers
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.CustomerName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.CustomerName);
                    break;
                case "Date":
                    customers = customers.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(s => s.CreatedDate);
                    break;

                default:
                    customers = customers.OrderBy(s => s.CustomerName);
                    break;
            }

            int pageSize = 10;



            return View(await PaginatedListCustomer<Customer>.CreateAsync(customers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,CustomerAddress,PhoneNumber,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Customers.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerName,CustomerAddress,PhoneNumber,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!unitExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(unit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool unitExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
