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
    public class ContacteTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContacteTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContacteTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContacteTypes.Where(c => !c.status.Equals(ModelActivationStatus.Delete.ToString())).ToListAsync());
        }

        // GET: ContacteTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacteType = await _context.ContacteTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contacteType == null)
            {
                return NotFound();
            }

            return View(contacteType);
        }

        // GET: ContacteTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContacteTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] ContacteType contacteType)
        {
            if (ModelState.IsValid)
            {
                contacteType.EnterBy = HttpContext.User.Identity.Name;
                _context.Add(contacteType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacteType);
        }

        // GET: ContacteTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacteType = await _context.ContacteTypes.FindAsync(id);
            if (contacteType == null)
            {
                return NotFound();
            }
            return View(contacteType);
        }

        // POST: ContacteTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,ID")] ContacteType contacteType)
        {
            if (id != contacteType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contacteType.UpdateBy = HttpContext.User.Identity.Name;
                    _context.Update(contacteType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContacteTypeExists(contacteType.ID))
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
            return View(contacteType);
        }

        // GET: ContacteTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacteType = await _context.ContacteTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contacteType == null)
            {
                return NotFound();
            }

            return View(contacteType);
        }

        // POST: ContacteTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var contacteType = await _context.ContacteTypes.FindAsync(id);
            if (contacteType != null)
            {
                contacteType.DeleteBy = HttpContext.User.Identity.Name;
                _context.ContacteTypes.Remove(contacteType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContacteTypeExists(long id)
        {
            return _context.ContacteTypes.Any(e => e.ID == id);
        }
    }
}
