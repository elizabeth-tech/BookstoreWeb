using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreWeb.Models.Entities
{
    public class Book
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Annotation { get; set; }

        [Column(TypeName = "decimal(8, 2)")] // Указываем, что в БД будет хранится 18 знаков до, и 4 знака после запятой
        public decimal Price { get; set; }

        public string Publisher { get; set; } // Издательство

        public int PageCount { get; set; }

        public int Circulation { get; set; } // Тираж

        public int YearPublishing { get; set; } // Год издания

        public byte[] Image { get; set; }

        public long CategoryId { get; set; } // Внешний ключ категории

        public Category Category { get; set; } // Навигационное свойство
    }
}
