using System.Collections.Generic;
using System.Linq;

namespace BookstoreWeb.Models.Entities
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void RemoveLine(Book book) => Lines.RemoveAll(l => l.Book.Id == book.Id);

        public decimal ComputeTotalValue() => Lines.Sum(e => e.Book.Price * e.Quantity);

        public virtual void Clear() => Lines.Clear();

        public virtual void AddItem(Book book, int quantity)
        {
            CartLine line = Lines
                .Where(b => b.Book.Id == book.Id)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
                line.Quantity += quantity;
        }
    }
}
