using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentApp.Data;
using RentApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace RentApp.Controllers
{
    [Authorize]
    public class RentsController : Controller
    {
        private readonly RentDbContext _context;

        public RentsController(RentDbContext context)
        {
            _context = context;
        }

        // GET: Rents
        public async Task<IActionResult> Index()
        {
            var addClass = _context.Rents
            .Include(c => c.RentItems)
            .AsNoTracking();
            return View(await addClass.ToListAsync());
        }

        // GET: Rents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents
                 .Include(r => r.RentItems)
                .FirstOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: Rents/Create
        public IActionResult Create()
        {
            var idAndName = _context.RentItems
               .Where(x => x.Available == true)
               .Select(x => new { x.RentItemId, x.Name });

            ViewBag.RentItemId = new SelectList(idAndName, "RentItemId", "Name");

            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentId,FirstName,LastName,TelNum,RentDate,RentItemId")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                RentItems result = (from p in _context.RentItems
                             where p.RentItemId == rent.RentItemId
                                    select p).SingleOrDefault();
                result.Available = false;

                _context.SaveChanges();

                _context.Add(rent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RentItemId = new SelectList(_context.RentItems, "RentItemId", "Name");

            return View(rent);
        }

        // GET: Rents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }
          var idAndName = _context.RentItems
         .Where(x => x.Available == true)
         .Select(x => new { x.RentItemId, x.Name });

            ViewBag.RentItemId = new SelectList(idAndName, "RentItemId", "Name");

            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentId,FirstName,LastName,TelNum,RentDate,RentItemId")] Rent rent)
        {
            if (id != rent.RentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.RentId))
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
            ViewData["RentItemId"] = new SelectList(_context.RentItems, "RentItemId", "RentItemId", rent.RentItemId);
            return View(rent);
        }

        // GET: Rents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents
                .Include(r => r.RentItems)
                .FirstOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rent = await _context.Rents.FindAsync(id);
            _context.Rents.Remove(rent);

            RentItems results = (from p in _context.RentItems
                                 where p.RentItemId == rent.RentItemId
                                 select p).SingleOrDefault();
            results.Available = true;

            _context.SaveChanges();

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(int id)
        {
            return _context.Rents.Any(e => e.RentId == id);
        }
    }
}
