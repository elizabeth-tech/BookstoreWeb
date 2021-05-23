using BookstoreWeb.Models.Entities;
using System.Collections.Generic;

namespace BookstoreWeb.Models.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        void SaveCategory(Category category);

        Category DeleteCategory(long Id);
    }
}
