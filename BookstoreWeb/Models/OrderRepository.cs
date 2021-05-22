using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookstoreWeb.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookstoreDbContext context;

        // Когда Order читается из бд, то также будет загружаться коллекция, ассоциированная со свойством Lines и Book
        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Book);

        public OrderRepository(BookstoreDbContext context) => this.context = context;

        public void SaveOrder(Order order)
        {
            // Уведомляем EF, что объекты существуют и не должны сохраняться в базе данных до тех пор,
            // пока они не будут модифицированы
            context.AttachRange(order.Lines.Select(l => l.Book));

            if (order.Id == 0)
                context.Orders.Add(order);
            context.SaveChanges();
        }
    }
}
