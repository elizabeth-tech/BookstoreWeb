using BookstoreWeb.Models.Entities;
using System.Collections.Generic;

namespace BookstoreWeb.Models.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }

        void SaveBook(Book book);

        Book DeleteBook(long Id);
    }
}
