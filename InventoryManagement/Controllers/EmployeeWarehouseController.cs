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
    public class EmployeeWarehouseController : Controller
    {
        private readonly InventoryManagementContext _context;

        public EmployeeWarehouseController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: EmployeeWareHouses
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var employeewarehouses = from m in _context.EmployeeWareHouses
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        employeewarehouses = employeewarehouses.Where(s => s.EmployeeName.Contains(searchString));
        //    }

        //    return View(await employeewarehouses.ToListAsync());
        //    //return View(await _context.EmployeeWareHouses.ToListAsync());
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

            var employeewarehouses = from s in _context.EmployeeWareHouses.Include(d => d.Employee).Include(d => d.Warehouse).AsNoTracking()
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employeewarehouses = employeewarehouses.Where(s => s.Employee.EmployeeName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    employeewarehouses = employeewarehouses.OrderByDescending(s => s.Employee.EmployeeName);
                    break;
                case "Date":
                    employeewarehouses = employeewarehouses.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    employeewarehouses = employeewarehouses.OrderByDescending(s => s.CreatedDate);
                    break;

                default:
                    employeewarehouses = employeewarehouses.OrderBy(s => s.Employee.EmployeeName);
                    break;
            }

            int pageSize = 10;



            return View(await PaginatedListEmployeeWarehouse<EmployeeWareHouse>.CreateAsync(employeewarehouses.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: EmployeeWareHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeWarehouse = await _context.EmployeeWareHouses
                .Include(d => d.Employee)
                .Include(d => d.Warehouse).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeWarehouse == null)
            {
                return NotFound();
            }

            return View(employeeWarehouse);
        }

        // GET: EmployeeWareHouses/Create
        public async Task<IActionResult> Create()
        {
            var employees = await _context.Employees.OrderBy(t => t.EmployeeName).ToListAsync();
            var warehouses = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            var employeeVM = new EmployeeWarehouseVM
            {
                Employees = new SelectList(employees, "Id", "EmployeeName"),
                Warehouses = new SelectList(warehouses, "Id", "WarehouseName"),
            };
            return View(employeeVM);
        }

        // POST: EmployeeWareHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,WarehouseId,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] EmployeeWareHouse employeeWareHouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeWareHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeWareHouse);
        }

        // GET: EmployeeWareHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeWarehouse = await _context.EmployeeWareHouses.FindAsync(id);
            if (employeeWarehouse == null)
            {
                return NotFound();
            }
            var employees = await _context.Employees.OrderBy(t => t.EmployeeName).ToListAsync();
            var warehouses = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            var employeeVM = new EmployeeWarehouseVM
            {
                Employees = new SelectList(employees, "Id", "EmployeeName"),
                Warehouses = new SelectList(warehouses, "Id", "WarehouseName"),
                EmployeeWareHouse = employeeWarehouse
            };
            return View(employeeVM);
        }

        // POST: EmployeeWareHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,WarehouseId,CreatedDate,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] EmployeeWareHouse employeeWareHouse)
        {
            if (id != employeeWareHouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeWareHouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!employeeWarehouseExists(employeeWareHouse.Id))
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
            return View(employeeWareHouse);
        }

        // GET: EmployeeWareHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeWarehouse = await _context.EmployeeWareHouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeWarehouse == null)
            {
                return NotFound();
            }

            return View(employeeWarehouse);
        }

        // POST: EmployeeWareHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeWarehouse = await _context.EmployeeWareHouses.FindAsync(id);
            _context.EmployeeWareHouses.Remove(employeeWarehouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool employeeWarehouseExists(int id)
        {
            return _context.EmployeeWareHouses.Any(e => e.Id == id);
        }
    }
}
