using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using learnchess.Areas.Identity.Data;
using learnchess.Models;
using Microsoft.AspNetCore.Authorization;

namespace learnchess.Controllers
{
    [Authorize(Roles = "Admin,Moderator,User")]
    public class ContactusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contactus
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Contactus.ToListAsync());
        }

        // GET: Contactus/Details/5
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Contactus == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactus
                .FirstOrDefaultAsync(m => m.id == id);
            if (contactus == null)
            {
                return NotFound();
            }

            return View(contactus);
        }

        // GET: Contactus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,email,Contact")] Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                contactus.id = Guid.NewGuid().ToString();
                _context.Add(contactus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactus);
        }

        // GET: Contactus/Edit/5
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Contactus == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactus.FindAsync(id);
            if (contactus == null)
            {
                return NotFound();
            }
            return View(contactus);
        }

        // POST: Contactus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Edit(string id, [Bind("id,name,email,Contact")] Contactus contactus)
        {
            if (id != contactus.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactusExists(contactus.id))
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
            return View(contactus);
        }

        // GET: Contactus/Delete/5
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Contactus == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactus
                .FirstOrDefaultAsync(m => m.id == id);
            if (contactus == null)
            {
                return NotFound();
            }

            return View(contactus);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Contactus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Contactus'  is null.");
            }
            var contactus = await _context.Contactus.FindAsync(id);
            if (contactus != null)
            {
                _context.Contactus.Remove(contactus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactusExists(string id)
        {
          return _context.Contactus.Any(e => e.id == id);
        }
    }
}
