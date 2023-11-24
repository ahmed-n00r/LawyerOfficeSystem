using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthorizeLibrary.Data;
using DBModels.AppModels;
using DBModels.AppConstants;

namespace App.web.Controllers
{
    public class LowyersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LowyersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lowyers
        public async Task<IActionResult> Index()
        {
            return View(await _context.lowyers.Where(c => !c.status.Equals(ModelActivationStatus.Delete.ToString())).ToListAsync());
        }

        // GET: Lowyers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lowyer = await _context.lowyers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lowyer == null)
            {
                return NotFound();
            }

            return View(lowyer);
        }

        // GET: Lowyers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lowyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LowyerNo")] Lowyer lowyer)
        {
            if (ModelState.IsValid)
            {
                lowyer.EnterBy = HttpContext.User.Identity.Name;
                _context.Add(lowyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lowyer);
        }

        // GET: Lowyers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lowyer = await _context.lowyers.FindAsync(id);
            if (lowyer == null)
            {
                return NotFound();
            }
            return View(lowyer);
        }

        // POST: Lowyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,LowyerNo,ID")] Lowyer lowyer)
        {
            if (id != lowyer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lowyer.UpdateBy = HttpContext.User.Identity.Name;
                    _context.Update(lowyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LowyerExists(lowyer.ID))
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
            return View(lowyer);
        }

        // GET: Lowyers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lowyer = await _context.lowyers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lowyer == null)
            {
                return NotFound();
            }

            return View(lowyer);
        }

        // POST: Lowyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var lowyer = await _context.lowyers.FindAsync(id);
            if (lowyer != null)
            {
                lowyer.DeleteBy = HttpContext.User.Identity.Name;
                _context.lowyers.Remove(lowyer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LowyerExists(long id)
        {
            return _context.lowyers.Any(e => e.ID == id);
        }
    }
}
