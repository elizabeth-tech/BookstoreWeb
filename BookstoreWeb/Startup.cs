using BookstoreWeb.Models;
using BookstoreWeb.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookstoreWeb
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreDbContext>(opts => {
                opts.UseSqlServer(Configuration["ConnectionStrings:BookstoreConnection"]);
            });

            // Добавление служб репозиториев
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Выводит указанную страницу книг заданного жанра (/Драма/Page2) +
                endpoints.MapControllerRoute("catpage", "{category}/Page{bookPage:int}",
                   new { Controller = "Home", action = "Index" });

                // Выводит первую страницу списка книг всех жанров (Раge1) +
                endpoints.MapControllerRoute("page", "Page{bookPage:int}",
                    new { Controller = "Home", action = "Index", bookPage = 1 });

                // Выводит первую страницу книг указанного жанра (/Драма) +
                endpoints.MapControllerRoute("category", "{category}",
                    new { Controller = "Home", action = "Index", bookPage = 1 });

                // Выводит первую страницу, отображая книги всех жанров (/Page1) -
                endpoints.MapControllerRoute("pagination", "Page{bookPage}",
                    new { Controller = "Home", action = "Index", bookPage = 1 });

                // Стандартный путь "{controller=Home}/{action=Index}/{id?}"
                endpoints.MapDefaultControllerRoute();
            });

            // Заполнение БД искусственными данными
            BookstoreInitializer initializer = new BookstoreInitializer();
            initializer.Initialize(app);
        }
    }
}
