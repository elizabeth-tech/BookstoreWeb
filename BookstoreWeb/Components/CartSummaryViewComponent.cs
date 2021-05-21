using BookstoreWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWeb.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart cart;

        public CartSummaryViewComponent(Cart cartService) => cart = cartService;

        public IViewComponentResult Invoke() => View(cart);
    }
}
