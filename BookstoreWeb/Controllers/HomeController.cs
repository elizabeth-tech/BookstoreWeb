using BookstoreWeb.Models.Interfaces;
using BookstoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookstoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository bookRepository;

        private readonly ICategoryRepository categoryRepository;

        public int PageSize = 12; // На каждой странице будет отображаться по 12 книг

        public HomeController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
        }

        public ViewResult Index(int bookPage = 1)
        {
            //return View(bookRepository.Books
            //    .OrderBy(b => b.Id)
            //    .Skip((bookPage - 1) * PageSize)
            //    .Take(PageSize));

            return View(new BooksListViewModel {                
                Categories = categoryRepository.Categories,

                Books = bookRepository.Books
                .OrderBy(b => b.Id)
                .Skip((bookPage - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = bookPage,
                    ItemsPerPage = PageSize,
                    TotalItems = bookRepository.Books.Count()
                }
            });
        }
    }
}
