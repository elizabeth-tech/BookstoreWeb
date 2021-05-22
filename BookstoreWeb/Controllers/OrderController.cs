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
                ModelState.AddModelError("", "Корзина пуста");

            // Если нет никаких проблем, то пользователю показываем страницу благодарности за заказ
            if (ModelState.IsValid) 
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));  
            }
            else
                return View(order); // Если есть проблемы, то остаемся на странице заказа
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
