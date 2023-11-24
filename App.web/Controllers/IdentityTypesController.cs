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
    public class IdentityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IdentityTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IdentityTypes.Where(e => !e.status.Equals(ModelActivationStatus.Delete)).ToListAsync());
        }

        // GET: IdentityTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityType = await _context.IdentityTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (identityType == null)
            {
                return NotFound();
            }

            return View(identityType);
        }

        // GET: IdentityTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IdentityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ID,status,EnterdDate,UpdateDate,DeleteDate,EnterBy,UpdateBy,DeleteBy")] IdentityType identityType)
        {
            if (ModelState.IsValid)
            {
                identityType.EnterBy = HttpContext.User.Identity.Name;
                _context.Add(identityType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(identityType);
        }

        // GET: IdentityTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityType = await _context.IdentityTypes.FindAsync(id);
            if (identityType == null)
            {
                return NotFound();
            }
            return View(identityType);
        }

        // POST: IdentityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,ID,status,EnterdDate,UpdateDate,DeleteDate,EnterBy,UpdateBy,DeleteBy")] IdentityType identityType)
        {
            if (id != identityType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    identityType.UpdateBy = HttpContext.User.Identity.Name;
                    _context.Update(identityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityTypeExists(identityType.ID))
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
            return View(identityType);
        }

        // GET: IdentityTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityType = await _context.IdentityTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (identityType == null)
            {
                return NotFound();
            }

            return View(identityType);
        }

        // POST: IdentityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var identityType = await _context.IdentityTypes.FindAsync(id);
            if (identityType != null)
            {
                identityType.DeleteBy = HttpContext.User.Identity.Name;
                _context.IdentityTypes.Remove(identityType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdentityTypeExists(long id)
        {
            return _context.IdentityTypes.Any(e => e.ID == id);
        }
    }
}
