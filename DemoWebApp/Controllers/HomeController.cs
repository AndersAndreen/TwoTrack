using DemoWebApp.Models;
using DemoWebApp.Services;
using DemoWebApp.Utils;
using DemoWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
namespace DemoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookQueryService _bookQueryService;

        public HomeController(ILogger<HomeController> logger, BookQueryService bookQueryService)
        {
            _logger = logger;
            _bookQueryService = bookQueryService;
        }

        public IActionResult Index(string searchfor)
        {
            var vm = (searchfor is null)
                ? new List<BookViewModel>()
                : _bookQueryService
                    .Get(book => book.Author.ToLower().Contains(searchfor) || book.Title.ToLower().Contains(searchfor),
                        BookViewModel.Map())
                    .ValueAndModelState(ModelState, new List<BookViewModel>());
            return View(vm);
        }

        public IActionResult DisplayBook(string id)
        {
            var vm = _bookQueryService
                .GetByIsbn(id, BookViewModel.Map())
                .ValueAndModelState(ModelState, new BookViewModel());
            return View(vm);
        }
        public IActionResult Selections()
        {
            var vm = _bookQueryService
                .Get(book => true, BookViewModel.Map())
                .ValueAndModelState(ModelState, new List<BookViewModel>());
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
