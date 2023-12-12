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
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
              return _context.Orders != null ? 
                          View(await _context.Orders.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Orders'  is null.");
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            return View(orderEntity);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,MFRName,OrderDescription,OrderStatus,OrderDate,OrderDeliveryDate,Products")] OrderEntity orderEntity)
        {
            if (ModelState.IsValid)
            {
                orderEntity.Id = Guid.NewGuid();
                _context.Add(orderEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderEntity);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            return View(orderEntity);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserName,MFRName,OrderDescription,OrderStatus,OrderDate,OrderDeliveryDate")] OrderEntity orderEntity)
        {
            if (id != orderEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderEntityExists(orderEntity.Id))
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
            return View(orderEntity);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            return View(orderEntity);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AppDbContext.Orders'  is null.");
            }
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity != null)
            {
                _context.Orders.Remove(orderEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderEntityExists(Guid id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
