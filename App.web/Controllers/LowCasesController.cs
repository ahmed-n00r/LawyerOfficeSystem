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
    public class LowCasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LowCasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LowCases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LowCases.Include(l => l.Claimant).Include(l => l.Lowyer).Where(c => !c.status.Equals(ModelActivationStatus.Delete.ToString()));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LowCases/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lowCase = await _context.LowCases
                .Include(l => l.Claimant)
                .Include(l => l.Lowyer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lowCase == null)
            {
                return NotFound();
            }

            return View(lowCase);
        }

        // GET: LowCases/Create
        public IActionResult Create()
        {
            ViewData["ClaimantId"] = new SelectList(_context.Claimants, "ID", "Name");
            ViewData["LoweyrId"] = new SelectList(_context.lowyers, "ID", "Name");
            return View();
        }

        // POST: LowCases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Case,CaseDoc,StartDate,ClaimantId,LoweyrId,ID")] LowCase lowCase)
        {
            if (ModelState.IsValid)
            {
                lowCase.EnterBy = HttpContext.User.Identity.Name;
                _context.Add(lowCase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaimantId"] = new SelectList(_context.Claimants, "ID", "Name", lowCase.ClaimantId);
            ViewData["LoweyrId"] = new SelectList(_context.lowyers, "ID", "Name", lowCase.LoweyrId);
            return View(lowCase);
        }

        // GET: LowCases/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lowCase = await _context.LowCases.FindAsync(id);
            if (lowCase == null)
            {
                return NotFound();
            }
            ViewData["ClaimantId"] = new SelectList(_context.Claimants, "ID", "Name", lowCase.ClaimantId);
            ViewData["LoweyrId"] = new SelectList(_context.lowyers, "ID", "Name", lowCase.LoweyrId);
            return View(lowCase);
        }

        // POST: LowCases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Case,CaseDoc,StartDate,EndDate,ClaimantId,LoweyrId,ID")] LowCase lowCase)
        {
            if (id != lowCase.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lowCase.UpdateBy = HttpContext.User.Identity.Name;
                    _context.Update(lowCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LowCaseExists(lowCase.ID))
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
            ViewData["ClaimantId"] = new SelectList(_context.Claimants, "ID", "Name", lowCase.ClaimantId);
            ViewData["LoweyrId"] = new SelectList(_context.lowyers, "ID", "Name", lowCase.LoweyrId);
            return View(lowCase);
        }

        // GET: LowCases/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lowCase = await _context.LowCases
                .Include(l => l.Claimant)
                .Include(l => l.Lowyer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lowCase == null)
            {
                return NotFound();
            }

            return View(lowCase);
        }

        // POST: LowCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var lowCase = await _context.LowCases.FindAsync(id);
            if (lowCase != null)
            {
                lowCase.DeleteBy = HttpContext.User.Identity.Name;
                _context.LowCases.Remove(lowCase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LowCaseExists(long id)
        {
            return _context.LowCases.Any(e => e.ID == id);
        }
    }
}
