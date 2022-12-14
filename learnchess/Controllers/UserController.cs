using learnchess.Data;
using Microsoft.AspNetCore.Mvc;

namespace learnchess.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;
        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> create([FromForm])
        {

        }*/
    }
}
