namespace BookstoreWeb.Models.Entities
{
    public class CartLine
    {
        public long Id { get; set; }

        public Book Book { get; set; }

        public int Quantity { get; set; }
    }
}
