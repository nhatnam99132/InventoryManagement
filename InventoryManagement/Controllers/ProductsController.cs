using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly InventoryManagementContext _context;

        public ProductsController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(
            string productCategory,
     string sortOrder,
     string currentFilter,
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
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var products = from s in _context.Products.Include(d => d.Category).Include(d => d.Unit).AsNoTracking()
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(productCategory))
            {
                products = products.Where(x => x.Category.CategoryName == productCategory);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.ProductName);
                    break;
                case "Date":
                    products = products.OrderBy(s => s.CreatedBy);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(s => s.CreatedBy);
                    break;
                
                default:
                    products = products.OrderBy(s => s.ProductName);
                    break;
            }
            IQueryable<string> cataQuery = from m in _context.Categories
                                                             orderby m.CategoryName
                                                             select m.CategoryName;
            ViewData["Categories"] = cataQuery;
            int pageSize = 10;
            SelectList Categories = new SelectList(await cataQuery.Distinct().ToListAsync());
            //var productCategoryVM = new ProductCategoryViewModel
            //{
            //    Categories = new SelectList(await cataQuery.Distinct().ToListAsync()),
            //    Products = await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize)
            //};

            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize, Categories));
        }



        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(d => d.Unit)
                .Include(d => d.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
          
            var cate = await _context.Categories.OrderBy(t => t.CategoryName).ToListAsync();
            var unit = await _context.Units.OrderBy(t => t.UnitName).ToListAsync();

            var productCategoryVM = new ProductCategoryViewModel
            {
                Units = new SelectList(unit, "Id", "UnitName"),
                Categories = new SelectList(cate, "Id", "CategoryName"),
            };


            //ViewData["Unit"] = new SelectList(unit, "Id", "UnitName");
            //ViewData["Category"] = new SelectList(cate, "Id", "CategoryName");
            return View(productCategoryVM);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,UnitId,CategoryId,Length,Width,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(d => d.Unit)
                .Include(d => d.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var cate = await _context.Categories.OrderBy(t => t.CategoryName).ToListAsync();
            var unit = await _context.Units.OrderBy(t => t.UnitName).ToListAsync();
            var productCategoryVM = new ProductCategoryViewModel
            {
                Units = new SelectList(unit, "Id", "UnitName"),
                Categories = new SelectList(cate, "Id", "CategoryName"),
                Product = product
            };
            return View(productCategoryVM);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,UnitId,CategoryId,Length,Width,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(product.Id))
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
            return View(product);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(d => d.Unit)
                .Include(d => d.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
