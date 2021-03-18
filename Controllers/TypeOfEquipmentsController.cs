using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentApp.Data;
using RentApp.Models;

namespace RentApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TypeOfEquipmentsController : Controller
    {
        private readonly RentDbContext _context;

        public TypeOfEquipmentsController(RentDbContext context)
        {
            _context = context;
        }

        // GET: TypeOfEquipments
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfEquipments.ToListAsync());
        }

        // GET: TypeOfEquipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfEquipment = await _context.TypeOfEquipments
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (typeOfEquipment == null)
            {
                return NotFound();
            }

            return View(typeOfEquipment);
        }

        // GET: TypeOfEquipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfEquipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentId,Name")] TypeOfEquipment typeOfEquipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfEquipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfEquipment);
        }

        // GET: TypeOfEquipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfEquipment = await _context.TypeOfEquipments.FindAsync(id);
            if (typeOfEquipment == null)
            {
                return NotFound();
            }
            return View(typeOfEquipment);
        }

        // POST: TypeOfEquipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipmentId,Name")] TypeOfEquipment typeOfEquipment)
        {
            if (id != typeOfEquipment.EquipmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfEquipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfEquipmentExists(typeOfEquipment.EquipmentId))
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
            return View(typeOfEquipment);
        }

        // GET: TypeOfEquipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfEquipment = await _context.TypeOfEquipments
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (typeOfEquipment == null)
            {
                return NotFound();
            }

            return View(typeOfEquipment);
        }

        // POST: TypeOfEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfEquipment = await _context.TypeOfEquipments.FindAsync(id);
            _context.TypeOfEquipments.Remove(typeOfEquipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfEquipmentExists(int id)
        {
            return _context.TypeOfEquipments.Any(e => e.EquipmentId == id);
        }
    }
}
