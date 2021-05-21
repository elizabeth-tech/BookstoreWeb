using BookstoreWeb.Models.Entities;

namespace BookstoreWeb.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}
