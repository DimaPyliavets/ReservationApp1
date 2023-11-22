using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationApp1.Models
{
    public class Cuisine
    {
        [Key]
        public int CuisineId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Cuisine")]
        public string CuisineName { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string CuisineDescription { get; set; }

        public ICollection<Menu> MenuItem { get; set; }
    }
}
