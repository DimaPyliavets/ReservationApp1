using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReservationApp1.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        //[ForeignKey("ZoneId")]
        //public int ZoneId { get; set; }
        //public Zone Zone { get; set; }

        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
