using BookstoreWeb.Models.Entities;
using System.Linq;

namespace BookstoreWeb.Models.Interfaces
{
    interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
    }
}
