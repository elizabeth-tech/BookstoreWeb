using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookstoreWeb.Models
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext context;

        public IQueryable<Book> Books => context.Books;

        public BookstoreRepository(BookstoreDbContext context) => this.context = context;

        public void SaveBook(Book book) => context.SaveChanges();

        public void CreateBook(Book book)
        {
            context.Add(book);
            context.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            context.Remove(book);
            context.SaveChanges();
        }
    }
}
