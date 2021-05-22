using System.ComponentModel.DataAnnotations;

namespace BookstoreWeb.Models.Entities
{
    public class Category
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
