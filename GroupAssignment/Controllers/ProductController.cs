using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupAssignment;
using GroupAssignment.Models;

namespace GroupAssignment.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Products'  is null.");
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ProductQuantity,ProductDescription")] ProductEntity productEntity)
        {
            if (ModelState.IsValid)
            {
                productEntity.Id = Guid.NewGuid();
                _context.Add(productEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productEntity);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }
            return View(productEntity);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductName,ProductQuantity,ProductDescription")] ProductEntity productEntity)
        {
            if (id != productEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductEntityExists(productEntity.Id))
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
            return View(productEntity);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }


        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products' is null.");
            }

            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            // Retrieve all orders that may contain the product
            var ordersWithProduct = await _context.Orders
                .Where(o => o.Products.Any(p => p.Id == id))
                .ToListAsync();

            foreach (var order in ordersWithProduct)
            {
                // Remove the product from the order
                var productToRemove = order.Products.FirstOrDefault(p => p.Id == id);
                if (productToRemove != null)
                {
                    order.Products.Remove(productToRemove);
                }
            }

            // Now remove the product itself
            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductEntityExists(Guid id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
