using BookstoreWeb.Models.Entities;
using System.Linq;

namespace BookstoreWeb.Models.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
    }
}
