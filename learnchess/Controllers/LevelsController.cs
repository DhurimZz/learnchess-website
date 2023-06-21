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
using ContosoUniversity;

namespace learnchess.Controllers
{
    public class LevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Levels
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "level_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var levels = from a in _context.Levels
                          select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                levels = levels.Where(a => a.Level.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "level_desc":
                    levels = levels.OrderByDescending(a => a.Level);
                    break;
                default:
                    levels = levels.OrderBy(a => a.Level);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Levels>.CreateAsync(levels.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Levels/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var levels = await _context.Levels
                .FirstOrDefaultAsync(m => m.LevelId == id);
            if (levels == null)
            {
                return NotFound();
            }

            return View(levels);
        }

        // GET: Levels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LevelId,Level")] Levels levels)
        {
            if (ModelState.IsValid)
            {
                levels.LevelId = Guid.NewGuid().ToString(); 
                _context.Add(levels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(levels);
        }

        // GET: Levels/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var levels = await _context.Levels.FindAsync(id);
            if (levels == null)
            {
                return NotFound();
            }
            return View(levels);
        }

        // POST: Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LevelId,Level")] Levels levels)
        {
            if (id != levels.LevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(levels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LevelsExists(levels.LevelId))
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
            return View(levels);
        }

        // GET: Levels/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var levels = await _context.Levels
                .FirstOrDefaultAsync(m => m.LevelId == id);
            if (levels == null)
            {
                return NotFound();
            }

            return View(levels);
        }

        // POST: Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Levels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Levels'  is null.");
            }
            var levels = await _context.Levels.FindAsync(id);
            if (levels != null)
            {
                _context.Levels.Remove(levels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LevelsExists(string id)
        {
          return _context.Levels.Any(e => e.LevelId == id);
        }
    }
}
