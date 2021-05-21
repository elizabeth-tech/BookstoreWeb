using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookstoreWeb.Models
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext context;

        public IEnumerable<Book> Books => context.Books.Include(c => c.Category);

        public BookRepository(BookstoreDbContext context) => this.context = context;

        public void SaveBook() => context.SaveChanges();

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
