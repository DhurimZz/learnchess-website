using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using learnchess.Areas.Identity.Data;
using learnchess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ContosoUniversity;


namespace learnchess.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RolesController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "role_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var roles = from a in _context.Roles
                        select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                roles = roles.Where(a => a.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "level_desc":
                    roles = roles.OrderByDescending(a => a.Name);
                    break;
                default:
                    roles = roles.OrderBy(a => a.Name);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<IdentityRole>.CreateAsync(roles.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if(roleName != null)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };
                _ = await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComfirmedDelete(string id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Roles'  is null.");
            }
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
