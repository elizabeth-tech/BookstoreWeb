using BookstoreWeb.Models;
using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            // ���������� ����� ������������
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // ��������� ��������� �������
            services.AddDistributedMemoryCache();
            services.AddSession();

            // ��� �������������� ��������� �������� � ����������� Cart ������ ����������� ���� � ��� �� ������
            services.AddScoped(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // ������� ��������� �������� ���� ��������� ����� (/�����/Page2) +
                endpoints.MapControllerRoute("catpage", "{category}/Page{bookPage:int}",
                   new { Controller = "Home", action = "Index" });

                // ������� ������ �������� ������ ���� ���� ������ (��ge1) +
                endpoints.MapControllerRoute("page", "Page{bookPage:int}",
                    new { Controller = "Home", action = "Index", bookPage = 1 });

                // ������� ������ �������� ���� ���������� ����� (/�����) +
                endpoints.MapControllerRoute("category", "{category}",
                    new { Controller = "Home", action = "Index", bookPage = 1 });

                // ������� ������ ��������, ��������� ����� ���� ������ (/Page1) -
                endpoints.MapControllerRoute("pagination", "Page{bookPage}",
                    new { Controller = "Home", action = "Index", bookPage = 1 });

                // ����������� ���� "{controller=Home}/{action=Index}/{id?}"
                endpoints.MapDefaultControllerRoute();
            });

            // ���������� �� �������������� �������
            //BookstoreInitializer initializer = new BookstoreInitializer();
            //initializer.Initialize(app);
            BookstoreInitializerProduction.SeedData(app);
        }
    }
}
