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
    public class PurchaseDetailController : Controller
    {
        private readonly InventoryManagementContext _context;

        public PurchaseDetailController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: PurchaseDetailDetails
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var purchaseorderdetails = from m in _context.PurchaseDetailDetails
        //                     select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        purchaseorderdetails = purchaseorderdetails.Where(s => s.CustomerName.Contains(searchString));
        //    }

        //    return View(await purchaseorderdetails.ToListAsync());
        //    //return View(await _context.PurchaseDetailDetails.ToListAsync());
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

            var purchaseorderdetails = from s in _context.PurchaseDetails.Include(d => d.Product).Include(d => d.Po).ThenInclude(d => d.Supplier).Include(d => d.Po).ThenInclude(d => d.Warehouse).AsNoTracking()
                                   select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                purchaseorderdetails = purchaseorderdetails.Where(s => s.Po.Supplier.SupplierName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    purchaseorderdetails = purchaseorderdetails.OrderByDescending(s => s.Po.Supplier.SupplierName);
                    break;
                case "Date":
                    purchaseorderdetails = purchaseorderdetails.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    purchaseorderdetails = purchaseorderdetails.OrderByDescending(s => s.CreatedDate);
                    break;

                default:
                    purchaseorderdetails = purchaseorderdetails.OrderBy(s => s.Po.Supplier.SupplierName);
                    break;
            }

            int pageSize = 10;



            return View(await PaginatedListPurchaseDetail<PurchaseDetail>.CreateAsync(purchaseorderdetails.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: PurchaseDetailDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PurchaseDetail = await _context.PurchaseDetails
                .Include(d => d.Po).ThenInclude(d => d.Supplier).Include(d => d.Po).ThenInclude(d => d.Warehouse).Include(d => d.Product).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (PurchaseDetail == null)
            {
                return NotFound();
            }

            return View(PurchaseDetail);
        }
        [HttpGet]
        public async Task<IActionResult> GetWarehouse(int saleOrderId)
        {
            var warehouse = await _context.PurchaseOrders.Include(d => d.Warehouse).FirstOrDefaultAsync(i => i.Id == saleOrderId);
            if (warehouse == null)
                return NotFound();
            return Ok(warehouse);
        }
        [HttpPost]
        public async Task<IActionResult> InsertInventory(int productId, int warehouseId, int quantity)
        {

            var inventoryProduct = new Inventory
            {
                ProductId = productId,
                WarehouseId = warehouseId,
                Quantity = quantity,
            };;

                    _context.Inventories.Add(inventoryProduct);
                    await _context.SaveChangesAsync();         
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInventory(int productId, int warehouseId, int quantity)
        {

            var inventoryProduct = await _context.Inventories.FirstOrDefaultAsync(i => (i.ProductId == productId) && (i.WarehouseId == warehouseId));
            if (inventoryProduct == null)
                return NotFound();
            inventoryProduct.Quantity = quantity;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Inventories.Update(inventoryProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseDetailExists(inventoryProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return NoContent();
        }
        // GET: PurchaseDetailDetails/Create
        public async Task<IActionResult> Create(int? idProduct)
        {
            
            var products = await _context.Products.OrderBy(t => t.ProductName).ToListAsync();
            var purchaseorders = await _context.PurchaseOrders.OrderBy(t => t.Id).ToListAsync();
            var inventoryProduct = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == idProduct);
            var purchaseDetailVM = new PurchaseDetailVM
            {
                Products = new SelectList(products, "Id", "ProductName"),
                PurchaseOrders = new SelectList(purchaseorders, "Id", "Id"),
                Inventory = inventoryProduct

            };
            return View(purchaseDetailVM);
        }

        // POST: PurchaseDetailDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Poid,ProductId,Quantity,Price,Discount,Vat,PriceAfterDiscount,TotalAmount,CreatedBy,UpdatedBy")] PurchaseDetail purchaseDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseDetail);
        }

        // GET: PurchaseDetailDetails/Edit/5
        public async Task<IActionResult> Edit(int? id, int? idProduct)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetail = await _context.PurchaseDetails.FindAsync(id);
            if (purchaseDetail == null)
            {
                return NotFound();
            }
            var products = await _context.Products.OrderBy(t => t.ProductName).ToListAsync();
            var purchaseorders = await _context.PurchaseOrders.OrderBy(t => t.Id).ToListAsync();
            var inventoryProduct = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == idProduct);
            var purchaseDetailVM = new PurchaseDetailVM
            {
                Products = new SelectList(products, "Id", "ProductName"),
                PurchaseOrders = new SelectList(purchaseorders, "Id", "Id"),
                PurchaseDetail = purchaseDetail,
                Inventory = inventoryProduct

            };
            //var customer = await _context.Customers.OrderBy(t => t.CustomerName).ToListAsync();
            //var warehouse = await _context.Warehouses.OrderBy(t => t.WarehouseName).ToListAsync();

            //var purchaseDetailVM = new PurchaseDetailVM
            //{
            //    Customers = new SelectList(customer, "Id", "CustomerName"),
            //    Warehouses = new SelectList(warehouse, "Id", "WarehouseName"),
            //    PurchaseDetail = PurchaseDetail
            //};
            return View(purchaseDetailVM);
        }

        // POST: PurchaseDetailDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Poid,ProductId,Quantity,Price,Discount,Vat,PriceAfterDiscount,TotalAmount,CreatedDate,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] PurchaseDetail purchaseDetail)
        {
            if (id != purchaseDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseDetailExists(purchaseDetail.Id))
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
            return View(purchaseDetail);
        }

        // GET: PurchaseDetailDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PurchaseDetail = await _context.PurchaseDetails.Include(d => d.Po).ThenInclude(d => d.Supplier).Include(d => d.Po).ThenInclude(d => d.Warehouse).Include(d => d.Product).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (PurchaseDetail == null)
            {
                return NotFound();
            }

            return View(PurchaseDetail);
        }

        // POST: PurchaseDetailDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var PurchaseDetail = await _context.PurchaseDetails.FindAsync(id);
            _context.PurchaseDetails.Remove(PurchaseDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseDetailExists(int id)
        {
            return _context.PurchaseDetails.Any(e => e.Id == id);
        }
    }
}
