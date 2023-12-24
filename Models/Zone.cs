using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationApp1.Models
{
    public class Zone
    {
        [Key]
        public int ZoneId { get; set; }

        [DisplayName("Name of Zone")]
        public string ZoneName { get; set; }

        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [DisplayName("Capacity")]
        public int Capacity { get; set; }

        //public ICollection<Tables> Tables { get; set; }
    }
}
