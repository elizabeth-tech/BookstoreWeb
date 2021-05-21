using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using BookstoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookstoreWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly Cart cart;

        public CartController(IBookRepository bookRepository, Cart cart)
        {
            this.bookRepository = bookRepository;
            this.cart = cart;
        }

        // Извлекает объект Cart из состояния сеанса и использует
        // его при создании объекта CartIndexViewModel, который затем передается методу
        // View() для применения в качестве модели представления.
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(long Id, string returnUrl)
        {
            Book book = bookRepository.Books.FirstOrDefault(b => b.Id == Id);

            if (book != null)
                cart.AddItem(book, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(long Id, string returnUrl)
        {
            Book book = bookRepository.Books.FirstOrDefault(b => b.Id == Id);

            if (book != null)
                cart.RemoveLine(book);

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
