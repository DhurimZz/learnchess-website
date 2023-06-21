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
using Microsoft.AspNetCore.Authorization;

namespace learnchess.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ArticlePage(string sortOrder, string currentFilter,string currentFilter1,string searchString,string selectString, int? pageNumber)
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

            var articles = from a in _context.Article
                           select a;


            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(a => a.Title.Contains(searchString));
            }

            /*if (!String.IsNullOrEmpty(selectString))
            {
                articles = articles.Where(a => a.AuthorId == selectString)
            .ToList();
            }*/
            
            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(a => a.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    articles = articles.OrderByDescending(a => a.Title);
                    break;
                default:
                    articles = articles.OrderBy(a => a.Title);
                    break;
            }
            int pageSize = 6;
            var paginatedList = await PaginatedList<Article>.CreateAsync(articles.AsNoTracking(), pageNumber ?? 1, pageSize);
            return Ok(paginatedList);
        }

        // GET: Articles
        
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

            var articles = from a in _context.Article
                           select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(a => a.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    articles = articles.OrderByDescending(a => a.Title);
                    break;
                default:
                    articles = articles.OrderBy(a => a.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Article>.CreateAsync(articles.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Articles/Details/5
      
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            var authors = _context.authors.ToList();
            ViewBag.Authors = authors;

            var levels = _context.Levels.ToList();
            ViewBag.Levels = levels;

            var languages = _context.Language.ToList();
            ViewBag.Languages = languages;

            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create([Bind("ArticleId,LevelId,LanguageId,Photo,Title,Description,url,AuthorId")] Article article)
        {

            if (ModelState.IsValid)
            {
                article.ArticleId = Guid.NewGuid().ToString();
                var a = await _context.authors.FindAsync(article.AuthorId);
                article.AuthorId = a.AuthorId;
                article.Author = a;

                var lvl = await _context.Levels.FindAsync(article.LevelId);
                article.LevelId = lvl.LevelId;
                article.Level = lvl;

                var l = await _context.Language.FindAsync(article.LanguageId);
                article.LanguageId = l.LanguageId;
                article.Language = l;

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
       
        public async Task<IActionResult> Edit(string? id)
        {
            var authors = _context.authors.ToList();
            ViewBag.Authors = authors;
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(string id, [Bind("ArticleId,Photo,Title,Description,url,AuthorId")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var a = await _context.authors.FindAsync(article.AuthorId);
                    article.AuthorId = a.AuthorId;
                    article.Author = a;

                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
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
            return View(article);
        }

        // GET: Articles/Delete/5
       
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Article == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Article'  is null.");
            }
            var article = await _context.Article.FindAsync(id);
            if (article != null)
            {
                _context.Article.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(string id)
        {
            return _context.Article.Any(e => e.ArticleId == id);
        }
    }
}
