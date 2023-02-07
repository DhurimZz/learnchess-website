using learnchess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learnchess.Controllers
{
    public class viewModelController : Controller
    {
        // GET: viewModelController
        public IActionResult Index()
        {
            ViewModel viewModel = new ViewModel
            {
                Author = new Author(),
                Article = new Article()
            };
            return View(viewModel);
        }
    }
}
