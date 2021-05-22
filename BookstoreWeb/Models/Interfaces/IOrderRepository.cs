using BookstoreWeb.Models.Entities;
using System.Linq;

namespace BookstoreWeb.Models.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}
