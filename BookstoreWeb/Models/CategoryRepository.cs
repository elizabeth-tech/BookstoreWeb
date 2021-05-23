using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookstoreWeb.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookstoreDbContext context;

        public IEnumerable<Category> Categories => context.Categories;

        public CategoryRepository(BookstoreDbContext context) => this.context = context;

        // Сохранение категории после редактирования старой или создания новой
        public void SaveCategory(Category category)
        {
            if (category.Id == 0)
                context.Categories.Add(category);
            else
            {
                Category dbEntry = context.Categories.FirstOrDefault(c => c.Id == category.Id);

                if (dbEntry != null)
                    dbEntry.Name = category.Name;
            }
            context.SaveChanges();
        }

        public Category DeleteCategory(long Id)
        {
            Category dbEntry = context.Categories.FirstOrDefault(c => c.Id == Id);
            if (dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
