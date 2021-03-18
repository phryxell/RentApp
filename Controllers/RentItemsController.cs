using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentApp.Data;
using RentApp.Models;

namespace RentApp.Controllers
{
    public class RentItemsController : Controller
    {
        private readonly RentDbContext _context;

        public RentItemsController(RentDbContext context)
        {
            _context = context;
        }

        // GET: RentItems
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortName"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewData["SortEquipName"] = String.IsNullOrEmpty(sortOrder) ? "TypeOfEquipment.Name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            var searchApp = from m in _context.RentItems.Include(c => c.ImageModal).Include(c => c.TypeOfEquipment)
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchApp = searchApp.Where(s => s.Name.ToLower().Contains(searchString.ToLower()) || s.TypeOfEquipment.Name.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "Name_desc":
                    searchApp = searchApp.OrderByDescending(s => s.Name);
                    break;
                case "TypeOfEquipment.Name_desc":
                    searchApp = searchApp.OrderByDescending(s => s.TypeOfEquipment.Name);
                    break;
                default:
                    searchApp = searchApp.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<RentItems>.CreateAsync(searchApp.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: RentItems/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentItems = await _context.RentItems.
                Include(c => c.ImageModal).Include(c => c.TypeOfEquipment)
                .FirstOrDefaultAsync(m => m.RentItemId == id);
            if (rentItems == null)
            {
                return NotFound();
            }

            return View(rentItems);
        }

        // GET: RentItems/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.TypeOfEquipments, "EquipmentId", "Name");
            ViewData["ImageId"] = new SelectList(_context.ImageModals, "ImageId", "Title");
            return View();
        }

        // POST: RentItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("RentItemId,Name,Available,EquipmentId,ImageId")] RentItems rentItems)
        {
            if (ModelState.IsValid)

            {
                _context.Add(rentItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rentItems);
        }

        // GET: RentItems/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentItems = await _context.RentItems.FindAsync(id);
            if (rentItems == null)
            {
                return NotFound();
            }

            ViewData["EquipmentId"] = new SelectList(_context.TypeOfEquipments, "EquipmentId", "Name");
            ViewData["ImageId"] = new SelectList(_context.ImageModals, "ImageId", "Title");
            return View(rentItems);
        }

        // POST: RentItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("RentItemId,Name,Available,EquipmentId,ImageId")] RentItems rentItems)
        {
            if (id != rentItems.RentItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentItemsExists(rentItems.RentItemId))
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
            return View(rentItems);
        }

        // GET: RentItems/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentItems = await _context.RentItems.
                 Include(c => c.ImageModal).Include(c => c.TypeOfEquipment)
                .FirstOrDefaultAsync(m => m.RentItemId == id);
            if (rentItems == null)
            {
                return NotFound();
            }

            return View(rentItems);
        }

        // POST: RentItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentItems = await _context.RentItems.FindAsync(id);
            _context.RentItems.Remove(rentItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentItemsExists(int id)
        {
            return _context.RentItems.Any(e => e.RentItemId == id);
        }
    }
}
