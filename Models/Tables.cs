using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReservationApp1.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        [Required]
        public string TableNumber { get; set; }

        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
