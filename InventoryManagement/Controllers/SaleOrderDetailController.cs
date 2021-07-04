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
    public class SaleOrderDetailController : Controller
    {
        private readonly InventoryManagementContext _context;

        public SaleOrderDetailController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: SaleOrderDetailDetails
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var saleorderdetails = from m in _context.SaleOrderDetailDetails
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        saleorderdetails = saleorderdetails.Where(s => s.CustomerName.Contains(searchString));
        //    }

        //    return View(await saleorderdetails.ToListAsync());
        //    //return View(await _context.SaleOrderDetailDetails.ToListAsync());
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

            var saleorderdetails = from s in _context.SaleOrderDetails.Include(d => d.Product).Include(d => d.So).ThenInclude(d => d.Customer).Include(d => d.So).ThenInclude(d => d.Warehouse).AsNoTracking()
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                saleorderdetails = saleorderdetails.Where(s => s.So.Customer.CustomerName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    saleorderdetails = saleorderdetails.OrderByDescending(s => s.So.Customer.CustomerName);
                    break;
                case "Date":
                    saleorderdetails = saleorderdetails.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    saleorderdetails = saleorderdetails.OrderByDescending(s => s.CreatedDate);
                    break;

                default:
                    saleorderdetails = saleorderdetails.OrderBy(s => s.So.Customer.CustomerName);
                    break;
            }

            int pageSize = 10;



            return View(await PaginatedListSaleOrderDetail<SaleOrderDetail>.CreateAsync(saleorderdetails.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> GetInventory(int productId, int warehouseId)
        {
            var inventoryProduct = await _context.Inventories.FirstOrDefaultAsync(i => (i.ProductId == productId) && (i.WarehouseId == warehouseId));
            if (inventoryProduct == null)
                return NotFound();
            return Ok(inventoryProduct);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductList(int warehouseId)
        {
            var products = await _context.Inventories.Include(d => d.Product).Where(i => i.WarehouseId == warehouseId).ToListAsync();
            if (products == null)
                return NotFound();
            return Ok(products);
        }
        [HttpGet]
        public async Task<IActionResult> GetWarehouse(int saleOrderId)
        {
            var warehouse = await _context.SaleOrders.Include(d => d.Warehouse).FirstOrDefaultAsync(i => i.Id == saleOrderId);
            if (warehouse == null)
                return NotFound();
            return Ok(warehouse);
        }
        // GET: SaleOrderDetailDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SaleOrderDetail = await _context.SaleOrderDetails
                .Include(d => d.So).ThenInclude(d => d.Customer).Include(d => d.So).ThenInclude(d => d.Warehouse).Include(d => d.Product).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (SaleOrderDetail == null)
            {
                return NotFound();
            }

            return View(SaleOrderDetail);
        }

        // GET: SaleOrderDetailDetails/Create
        public async Task<IActionResult> Create()
        {
            var products = await _context.Products.OrderBy(t => t.ProductName).ToListAsync();
            var saleorders = await _context.SaleOrders.OrderBy(t => t.Id).ToListAsync();

            var saleOrderDetailVM = new SaleOrderDetailVM
            {
                Products = new SelectList(products, "Id", "ProductName"),
                SaleOrders = new SelectList(saleorders, "Id", "Id"),

            };
            return View(saleOrderDetailVM);
        }

        // POST: SaleOrderDetailDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Soid,ProductId,Quantity,Price,Discount,Vat,PriceAfterDiscount,TotalAmount,CreatedBy,UpdatedBy")] SaleOrderDetail saleOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleOrderDetail);
        }

        // GET: SaleOrderDetailDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
 
            if (id == null)
            {
                return NotFound();
            }

            var saleOrderDetail = await _context.SaleOrderDetails.FindAsync(id);
            if (saleOrderDetail == null)
            {
                return NotFound();
            }
            var products = await _context.Products.OrderBy(t => t.ProductName).ToListAsync();
            var saleorders = await _context.SaleOrders.OrderBy(t => t.Id).ToListAsync();

            var saleOrderDetailVM = new SaleOrderDetailVM
            {
                Products = new SelectList(products, "Id", "ProductName"),
                SaleOrders = new SelectList(saleorders, "Id", "Id"),
                SaleOrderDetail = saleOrderDetail,
        

            };
            //var customer = await _context.Customers.OrderBy(t => t.CustomerName).ToListAsync();
            //var warehouse = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            //var saleOrderDetailVM = new SaleOrderDetailVM
            //{
            //    Customers = new SelectList(customer, "Id", "CustomerName"),
            //    Warehouses = new SelectList(warehouse, "Id", "WarehouseName"),
            //    SaleOrderDetail = SaleOrderDetail
            //};
            return View(saleOrderDetailVM);
        }

        // POST: SaleOrderDetailDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Soid,ProductId,Quantity,Price,Discount,Vat,PriceAfterDiscount,TotalAmount,CreatedDate,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] SaleOrderDetail saleOrderDetail)
        {
            if (id != saleOrderDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleOrderDetailExists(saleOrderDetail.Id))
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
            return View(saleOrderDetail);
        }

        // GET: SaleOrderDetailDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SaleOrderDetail = await _context.SaleOrderDetails.Include(d => d.Product).Include(d => d.So).ThenInclude(d => d.Customer).Include(d => d.So).ThenInclude(d => d.Warehouse).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (SaleOrderDetail == null)
            {
                return NotFound();
            }

            return View(SaleOrderDetail);
        }

        // POST: SaleOrderDetailDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var SaleOrderDetail = await _context.SaleOrderDetails.FindAsync(id);
            _context.SaleOrderDetails.Remove(SaleOrderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleOrderDetailExists(int id)
        {
            return _context.SaleOrderDetails.Any(e => e.Id == id);
        }
    }
}
