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
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Articles

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int? searchNumber)
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

            var books = from b in _context.books
                        select b;


            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Author.Name.Contains(searchString));
            }
            if (searchNumber != null )
            {
                books = books.Where(b => b.PublicationYear.Equals(searchNumber));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
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

            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create([Bind("BookId,Title,PublicationYear,AuthorId")] Book book)
        {

            if (ModelState.IsValid)
            {
                book.BookId = Guid.NewGuid().ToString();
                var a = await _context.authors.FindAsync(book.AuthorId);
                book.AuthorId = a.AuthorId;
                book.Author = a;

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Articles/Edit/5
       
        public async Task<IActionResult> Edit(string? id)
        {
            var authors = _context.authors.ToList();
            ViewBag.Authors = authors;
            if (id == null || _context.books == null)
            {
                return NotFound();
            }

            var book = await _context.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(string id, [Bind("BookId,Title,PublicationYear,AuthorId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var a = await _context.authors.FindAsync(book.AuthorId);
                    book.AuthorId = a.AuthorId;
                    book.Author = a;

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            return View(book);
        }

        // GET: Articles/Delete/5
       
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.books == null)
            {
                return NotFound();
            }

            var book = await _context.books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Article'  is null.");
            }
            var book = await _context.books.FindAsync(id);
            if (book != null)
            {
                _context.books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.books.Any(e => e.BookId == id);
        }
    }
}
