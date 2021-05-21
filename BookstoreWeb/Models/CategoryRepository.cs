using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using System.Collections.Generic;

namespace BookstoreWeb.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookstoreDbContext context;

        public IEnumerable<Category> Categories => context.Categories;

        public CategoryRepository(BookstoreDbContext context) => this.context = context;
    }
}
