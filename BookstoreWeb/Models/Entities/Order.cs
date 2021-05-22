using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookstoreWeb.Models.Entities
{
    public class Order
    {
        // Атрибут предотвращает предоставление пользователем значений для снабженных этим атрибутом свойств в HTTP-запросе
        [BindNever] 
        public long Id { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Postcode { get; set; }

        [BindNever]
        public bool Shipped { get; set; } // Отправлен заказ или нет
    }
}
