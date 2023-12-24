using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ReservationApp1.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Restaurant Name")]
        public string RestaurantName { get; set; }

        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        [DisplayName("Website")]
        public string WebPage { get; set; }

        [ForeignKey("CuisineId")]
        public int CuisineId { get; set; }
        public Cuisine Cuisine { get; set; }

        public ICollection<Zone> Zones { get; set; }

        public ICollection<Table> Tables { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Open Time")]
        public TimeSpan OpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Close Time")]
        public TimeSpan CloseTime { get; set; }

        [EnumDataType(typeof(DayOfWeek))]
        [DisplayName("Open Days")]
        public string OpenDays { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
