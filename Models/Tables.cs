using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReservationApp1.Models
{
    public class Tables
    {
        [Key]
        public int TableId { get; set; }

        [ForeignKey("ZoneId")]
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }

        //public ICollection<Reservation> Reservations { get; set; }
    }
}
