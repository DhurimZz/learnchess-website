using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using learnchess.Areas.Identity.Data;
using learnchess.Models;
using ContosoUniversity;

namespace learnchess.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Languages
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "language_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var languages = from a in _context.Language
                         select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                languages = languages.Where(a => a.Language.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "language_desc":
                    languages = languages.OrderByDescending(a => a.Language);
                    break;
                default:
                    languages = languages.OrderBy(a => a.Language);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Languages>.CreateAsync(languages.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Languages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Language == null)
            {
                return NotFound();
            }

            var languages = await _context.Language
                .FirstOrDefaultAsync(m => m.LanguageId == id);
            if (languages == null)
            {
                return NotFound();
            }

            return View(languages);
        }

        // GET: Languages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageId,Language")] Languages languages)
        {
            if (ModelState.IsValid)
            {
                languages.LanguageId = Guid.NewGuid().ToString();
                _context.Add(languages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(languages);
        }

        // GET: Languages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Language == null)
            {
                return NotFound();
            }

            var languages = await _context.Language.FindAsync(id);
            if (languages == null)
            {
                return NotFound();
            }
            return View(languages);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LanguageId,Language")] Languages languages)
        {
            if (id != languages.LanguageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(languages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguagesExists(languages.LanguageId))
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
            return View(languages);
        }

        // GET: Languages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Language == null)
            {
                return NotFound();
            }

            var languages = await _context.Language
                .FirstOrDefaultAsync(m => m.LanguageId == id);
            if (languages == null)
            {
                return NotFound();
            }

            return View(languages);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Language == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Language'  is null.");
            }
            var languages = await _context.Language.FindAsync(id);
            if (languages != null)
            {
                _context.Language.Remove(languages);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguagesExists(string id)
        {
          return _context.Language.Any(e => e.LanguageId == id);
        }
    }
}
