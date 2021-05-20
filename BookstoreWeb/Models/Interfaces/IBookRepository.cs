using BookstoreWeb.Models.Entities;
using System.Linq;

namespace BookstoreWeb.Models.Interfaces
{
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }
    }
}
