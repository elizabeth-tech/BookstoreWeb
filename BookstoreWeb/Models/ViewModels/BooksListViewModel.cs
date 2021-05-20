using BookstoreWeb.Models.Entities;
using System.Linq;

namespace BookstoreWeb.Models.ViewModels
{
    public class BooksListViewModel
    {
        public IQueryable<Book> Books { get; set; }

        public IQueryable<Category> Categories { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}
