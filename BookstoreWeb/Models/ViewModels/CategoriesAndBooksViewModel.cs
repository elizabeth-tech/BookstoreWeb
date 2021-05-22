using BookstoreWeb.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BookstoreWeb.Models.ViewModels
{
    public class CategoriesAndBooksViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public Book Book { get; set; }

        public IFormFile Image { get; set; }
    }
}
