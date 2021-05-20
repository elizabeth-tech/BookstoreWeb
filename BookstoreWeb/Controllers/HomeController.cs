using BookstoreWeb.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository bookRepository;

        public HomeController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public ViewResult Index()
        {
            return View(bookRepository.Books);
        }
    }
}
