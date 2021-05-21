using BookstoreWeb.Models.Entities;
using System.Collections.Generic;

namespace BookstoreWeb.Models.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
