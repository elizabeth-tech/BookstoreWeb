using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository repository;

        private readonly Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService) =>
            (repository, cart) = (repoService, cartService);

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count == 0)
                ModelState.AddModelError("", "В Вашей корзине нет ни одной книги, нам нечего отправить :(");

            if (ModelState.IsValid) // проверяем, есть ли какие нибудь проблемы
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.Id });
            }
            else
                return View();
        }
    }
}
