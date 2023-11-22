using System.ComponentModel.DataAnnotations;

namespace ReservationApp1.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int CuisineId { get; set; }
        public Cuisine Cuisine { get; set; }
    }
}
