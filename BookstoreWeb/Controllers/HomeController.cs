using BookstoreWeb.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookstoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository bookRepository;

        public int PageSize = 12; // На каждой странице будет отображаться по 12 книг

        public HomeController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public ViewResult Index(int bookPage = 1)
        {
            return View(bookRepository.Books
                .OrderBy(b => b.Id)
                .Skip((bookPage - 1) * PageSize)
                .Take(PageSize));
        }
    }
}
