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
    public class VideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> VideoPage(string sortOrder, string currentFilter, string currentFilter1, string searchString, string selectString, int? pageNumber)
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

            var videos = from v in _context.Videos
                           select v;


            if (!String.IsNullOrEmpty(searchString))
            {
                videos = videos.Where(v => v.Title.Contains(searchString));
            }

            /*if (!String.IsNullOrEmpty(selectString))
            {
                videos = videos.Where(a => a.AuthorId == selectString)
            .ToList();
            }*/

            if (!String.IsNullOrEmpty(searchString))
            {
                videos = videos.Where(v => v.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    videos = videos.OrderByDescending(v => v.Title);
                    break;
                default:
                    videos = videos.OrderBy(v => v.Title);
                    break;
            }
            int pageSize = 6;
            return View(await PaginatedList<Videos>.CreateAsync(videos.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Videos
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

            var videos = from v in _context.Videos
                           select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                videos = videos.Where(v => v.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    videos = videos.OrderByDescending(v => v.Title);
                    break;
                default:
                    videos = videos.OrderBy(v => v.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Videos>.CreateAsync(videos.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Videos == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos
                .FirstOrDefaultAsync(m => m.VideosId == id);
            if (videos == null)
            {
                return NotFound();
            }

            return View(videos);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            var videos = _context.authors.ToList();
            ViewBag.Authors = videos;

            var levels = _context.Levels.ToList();
            ViewBag.Levels = levels;

            var languages = _context.Language.ToList();
            ViewBag.Languages = languages;

            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideosId,LevelId,LanguageId,Video,Title,Description,url,AuthorId")] Videos videos)
        {

            if (ModelState.IsValid)
            {
                videos.VideosId = Guid.NewGuid().ToString();
                var a = await _context.authors.FindAsync(videos.AuthorId);
                videos.AuthorId = a.AuthorId;
                videos.Author = a;

                var lvl = await _context.Levels.FindAsync(videos.LevelId);
                videos.LevelId = lvl.LevelId;
                videos.Level = lvl;

                var l = await _context.Language.FindAsync(videos.LanguageId);
                videos.LanguageId = l.LanguageId;
                videos.Language = l;

                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        videos.Video = dataStream.ToArray();
                    }
                }
                _context.Add(videos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videos);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var authors = _context.authors.ToList();
            ViewBag.Authors = authors;
            if (id == null || _context.Videos == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos.FindAsync(id);
            if (videos == null)
            {
                return NotFound();
            }
            return View(videos);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VideosId,Video,Title,Description,Url")] Videos videos)
        {
            if (id != videos.VideosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var a = await _context.authors.FindAsync(videos.AuthorId);
                    videos.AuthorId = a.AuthorId;
                    videos.Author = a;
                    if (Request.Form.Files.Count > 0)
                    {
                        IFormFile file = Request.Form.Files.FirstOrDefault();
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            videos.Video = dataStream.ToArray();
                        }
                    }
                    _context.Update(videos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideosExists(videos.VideosId))
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
            return View(videos);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Videos == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos
                .FirstOrDefaultAsync(m => m.VideosId == id);
            if (videos == null)
            {
                return NotFound();
            }

            return View(videos);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Videos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Videos'  is null.");
            }
            var videos = await _context.Videos.FindAsync(id);
            if (videos != null)
            {
                _context.Videos.Remove(videos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideosExists(string id)
        {
          return _context.Videos.Any(e => e.VideosId == id);
        }
    }
}
