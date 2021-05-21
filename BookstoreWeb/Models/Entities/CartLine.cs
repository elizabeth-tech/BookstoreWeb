namespace BookstoreWeb.Models.Entities
{
    public class CartLine
    {
        public int CartLineID { get; set; }

        public Book Book { get; set; }

        public int Quantity { get; set; }
    }
}
