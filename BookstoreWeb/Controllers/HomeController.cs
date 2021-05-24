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

        public ViewResult Index(string category, int bookPage = 1)
        {
            return View(new BooksListViewModel {                
                Categories = categoryRepository.Categories,

                Books = bookRepository.Books
                .Where(b => category == null || b.Category.Name == category)
                .OrderBy(b => b.Id)
                .Skip((bookPage - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = bookPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? bookRepository.Books.Count() : bookRepository.Books.Where(e => e.Category.Name == category).Count()
                },

                CurrentCategory = category
            });
        }

        public ViewResult BookDescription(int Id)
        {
            return View(bookRepository.Books.FirstOrDefault(b => b.Id == Id));
        }
    }
}
