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
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GamesPage(string sortOrder, string currentFilter, string currentFilter1, string searchString, string selectString, int? pageNumber)
        {
            var authors = _context.authors.ToList();
            ViewBag.Authors = authors;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilter1"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var games = from a in _context.Games
                           select a;


            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(a => a.Title.Contains(searchString));
            }

            /*if (!String.IsNullOrEmpty(selectString))
            {
                articles = articles.Where(a => a.AuthorId == selectString)
            .ToList();
            }*/

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(a => a.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(a => a.Title);
                    break;
                default:
                    games = games.OrderBy(a => a.Title);
                    break;
            }
            int pageSize = 6;
            var paginatedList = await PaginatedList<Games>.CreateAsync(games.AsNoTracking(), pageNumber ?? 1, pageSize);
            return Ok(paginatedList);
        }

        // GET: Games
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var games = from a in _context.Games
                           select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(a => a.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(a => a.Title);
                    break;
                default:
                    games = games.OrderBy(a => a.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Games>.CreateAsync(games.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .FirstOrDefaultAsync(m => m.GamesId == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GamesId,Thumbnail,Title,Url")] Games games)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(games);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(games);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var games = await _context.Games.FindAsync(id);
            if (games == null)
            {
                return NotFound();
            }
            return View(games);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GamesId,Thumbnail,Title,Url")] Games games)
        {
            if (id != games.GamesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(games);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesExists(games.GamesId))
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
            return View(games);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .FirstOrDefaultAsync(m => m.GamesId == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Games'  is null.");
            }
            var games = await _context.Games.FindAsync(id);
            if (games != null)
            {
                _context.Games.Remove(games);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamesExists(int id)
        {
          return _context.Games.Any(e => e.GamesId == id);
        }
    }
}
