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
    public class ClaimantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Claimants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Claimants.Include(c => c.IdentityType).Where(c => !c.status.Equals(ModelActivationStatus.Delete.ToString()));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Claimants/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimant = await _context.Claimants
                .Include(c => c.IdentityType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (claimant == null)
            {
                return NotFound();
            }

            return View(claimant);
        }

        // GET: Claimants/Create
        public IActionResult Create()
        {
            ViewData["IdentityTypeId"] = new SelectList(_context.IdentityTypes, "ID", "Name");
            return View();
        }

        // POST: Claimants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Identity,IdentityTypeId,ID")] Claimant claimant)
        {
            if (ModelState.IsValid)
            {
                claimant.EnterBy = HttpContext.User.Identity.Name;
                _context.Add(claimant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityTypeId"] = new SelectList(_context.IdentityTypes, "ID", "Name", claimant.IdentityTypeId);
            return View(claimant);
        }

        // GET: Claimants/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimant = await _context.Claimants.FindAsync(id);
            if (claimant == null)
            {
                return NotFound();
            }
            ViewData["IdentityTypeId"] = new SelectList(_context.IdentityTypes, "ID", "Name", claimant.IdentityTypeId);
            return View(claimant);
        }

        // POST: Claimants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Identity,IdentityTypeId,ID")] Claimant claimant)
        {
            if (id != claimant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    claimant.UpdateBy = HttpContext.User.Identity.Name;
                    _context.Update(claimant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimantExists(claimant.ID))
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
            ViewData["IdentityTypeId"] = new SelectList(_context.IdentityTypes, "ID", "Name", claimant.IdentityTypeId);
            return View(claimant);
        }

        // GET: Claimants/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimant = await _context.Claimants
                .Include(c => c.IdentityType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (claimant == null)
            {
                return NotFound();
            }

            return View(claimant);
        }

        // POST: Claimants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var claimant = await _context.Claimants.FindAsync(id);
            if (claimant != null)
            {
                claimant.DeleteBy = HttpContext.User.Identity.Name;
                _context.Claimants.Remove(claimant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimantExists(long id)
        {
            return _context.Claimants.Any(e => e.ID == id);
        }
    }
}
