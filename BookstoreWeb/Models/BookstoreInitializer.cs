using BookstoreWeb.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace BookstoreWeb.Models
{
    // Класс для тестирования. Заполняет БД искусственными данными
    public class BookstoreInitializer
    {
        public void Initialize(IApplicationBuilder app)
        {
            BookstoreDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<BookstoreDbContext>();

            // Удаление существующей бд
            //await context.Database.EnsureDeletedAsync().ConfigureAwait(false);

            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate(); // Migrate - создает бд, если ее нет и накатывает все миграции

            if (context.Books.Any()) return; // Если есть хоть одна книга, то выходим из инициализатора

            // Заполняем бд данными
            InitializeCategories(context);
            InitializeBooks(context);
        }

        private const int categoriesCount = 5;
        private Category[] categories;
        private void InitializeCategories(BookstoreDbContext context)
        {
            var rnd = new Random();
            categories = new Category[categoriesCount];

            for (int i = 0; i < categoriesCount; i++)
                categories[i] = new Category { Name = $"Категория {i + 1}" };

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private void InitializeBooks(BookstoreDbContext context)
        {
            var rnd = new Random();
            byte[] imageBytes = File.ReadAllBytes(@"wwwroot\img\book.jpg");
            int booksCount = 30;

            Book[] books = Enumerable.Range(1, booksCount)
               .Select(i => new Book
               {
                   Name = $"MyBook {i}",
                   Author = $"Автор К.",

                   Annotation = $"Lorem ipsum, dolor sit amet consectetur adipisicing elit. Saepe " +
                   $"perferendis officiis consectetur dolorem pariatur ipsa soluta adipisci omnis molestiae" +
                   $" modi culpa ut blanditiis eum quaerat unde, sunt similique exercitationem optio?" +
                   $"Lorem ipsum dolor sit amet consectetur adipisicing elit.Quo," +
                   $"rem adipisci quasi et commodi suscipit vel neque blanditiis labore ut minima nam " +
                   $"dolores eligendi consequatur.Asperiores nobis voluptas officia quasi." +
                   $"Lorem ipsum dolor sit amet,consectetur adipisicing elit.Illo,facere eligendi.",

                   Category = categories[rnd.Next(1, categoriesCount)],
                   Price = 450,
                   Image = imageBytes,
                   PageCount = 680,
                   Publisher = $"Иностранка",
                   YearPublishing = 2007,
                   Circulation = 6000
               })
               .ToArray();

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
