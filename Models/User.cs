using System.ComponentModel.DataAnnotations;

namespace ReservationApp1.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string UserPhone { get; set; }
        public ICollection<Reservation> Rezervations { get; set; }
    }
}
