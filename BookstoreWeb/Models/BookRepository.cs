using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookstoreWeb.Models
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext context;

        public IEnumerable<Book> Books => context.Books
            .Include(c => c.Category)
            .ToArray();

        public BookRepository(BookstoreDbContext context) => this.context = context;

        // Сохранение книги после редактирования старой или создания новой
        public void SaveBook(Book book)
        {
            if (book.Id == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.FirstOrDefault(b => b.Id == book.Id);

                if (dbEntry != null)
                {
                    dbEntry.Name = book.Name;
                    dbEntry.Author = book.Author;
                    dbEntry.Annotation = book.Annotation;
                    dbEntry.Price = book.Price;
                    dbEntry.Publisher = book.Publisher;
                    dbEntry.PageCount = book.PageCount;
                    dbEntry.Circulation = book.Circulation;
                    dbEntry.YearPublishing = book.YearPublishing;
                    dbEntry.CategoryId = book.CategoryId;
                    dbEntry.Image = book.Image;
                }
            }
            context.SaveChanges();
        }

        public Book DeleteBook(long Id)
        {
            Book dbEntry = context.Books.FirstOrDefault(b => b.Id == Id);
            if (dbEntry != null)
            {
                context.Books.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
