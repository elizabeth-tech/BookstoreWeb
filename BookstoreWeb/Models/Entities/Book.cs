using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreWeb.Models.Entities
{
    public class Book
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Annotation { get; set; }

        [Required]
        [Column(TypeName = "decimal(8, 2)")] // Указываем, что в БД будет хранится 18 знаков до, и 4 знака после запятой
        public decimal Price { get; set; }

        [Required]
        public string Publisher { get; set; } // Издательство

        [Required]
        public int PageCount { get; set; }

        [Required]
        public int Circulation { get; set; } // Тираж

        [Required]
        public int YearPublishing { get; set; } // Год издания

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public long CategoryId { get; set; } // Внешний ключ категории

        [Required]
        public Category Category { get; set; } // Навигационное свойство
    }
}
