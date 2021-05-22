using BookstoreWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWeb.Models
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
