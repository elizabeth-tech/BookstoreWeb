using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using System.Linq;

namespace BookstoreWeb.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookstoreDbContext context;

        public IQueryable<Category> Categories => context.Categories;

        public CategoryRepository(BookstoreDbContext context) => this.context = context;
    }
}
