using BookstoreWeb.Models.Entities;
using System.Collections.Generic;

namespace BookstoreWeb.Models.ViewModels
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}
