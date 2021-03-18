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
    [Authorize(Roles = "Administrator")]
    public class ImageModalsController : Controller
    {
        private readonly RentDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageModalsController(RentDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: ImageModals
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImageModals.ToListAsync());
        }

        // GET: ImageModals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModal = await _context.ImageModals
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModal == null)
            {
                return NotFound();
            }

            return View(imageModal);
        }

        // GET: ImageModals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImageModals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,Title,ImageFile")] ImageModal imageModal)
        {
            if (ModelState.IsValid)
            {
                if (imageModal != null && imageModal.ImageFile != null)
                {
                    //Save Image to wwwroot/Image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(imageModal.ImageFile.FileName);
                    string extension = Path.GetExtension(imageModal.ImageFile.FileName);
                    imageModal.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await imageModal.ImageFile.CopyToAsync(fileStream);
                    }
                }
                //Insert record

                _context.Add(imageModal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageModal);
        }

        // GET: ImageModals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModal = await _context.ImageModals.FindAsync(id);
            if (imageModal == null)
            {
                return NotFound();
            }
            return View(imageModal);
        }

        // POST: ImageModals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Title,ImageName")] ImageModal imageModal)
        {
            if (id != imageModal.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageModal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageModalExists(imageModal.ImageId))
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
            return View(imageModal);
        }

        // GET: ImageModals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModal = await _context.ImageModals
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModal == null)
            {
                return NotFound();
            }

            return View(imageModal);
        }

        // POST: ImageModals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageModal = await _context.ImageModals.FindAsync(id);
            _context.ImageModals.Remove(imageModal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageModalExists(int id)
        {
            return _context.ImageModals.Any(e => e.ImageId == id);
        }
    }
}
