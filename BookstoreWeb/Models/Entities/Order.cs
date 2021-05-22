using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace BookstoreWeb.Models.Entities
{
    public class Order
    {
        // Атрибут предотвращает предоставление пользователем значений для снабженных этим атрибутом свойств в HTTP-запросе
        [BindNever] 
        public long Id { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string Postcode { get; set; }

        [BindNever]
        public bool Shipped { get; set; } // Отправлен заказ или нет
    }
}
