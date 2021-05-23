using System;
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
        [Range(1, 10000, ErrorMessage="Пожалуйста, укажите цену в пределах от 1 до 10 000 руб.")]
        [Column(TypeName = "decimal(8, 2)")] // Указываем, что в БД будет хранится 18 знаков до, и 4 знака после запятой
        public decimal Price { get; set; }

        [Required]
        public string Publisher { get; set; } // Издательство

        [Required]
        [Range(1, 5000, ErrorMessage = "Пожалуйста, введите целое число больше 1 и меньше 5000")]
        public int PageCount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите целое число больше 1")]
        public int Circulation { get; set; } // Тираж

        [Required]
        [Range(1900, 2021, ErrorMessage = "Пожалуйста, введите год в пределах от 1900 до 2021")]
        public int YearPublishing { get; set; } // Год издания

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public long CategoryId { get; set; } // Внешний ключ категории

        public Category Category { get; set; } // Навигационное свойство
    }
}
