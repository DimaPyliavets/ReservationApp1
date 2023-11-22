using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationApp1.Data;
using ReservationApp1.Models;

namespace ReservationApp1.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShowAllReservations()
        {
            List<Reservation> reservations = await _context.Reservations
                .Include(r => r.Restaurant)
                .Include(r => r.Zone)
                .Include(r => r.User)
                .ToListAsync();

            return View(reservations);
        }

        [HttpGet]
        public IActionResult MakeReservation()
        {
            List<Restaurant> restaurants = _context.Restaurants.ToList();
            ViewBag.Restaurants = new SelectList(restaurants, "RestaurantId", "RestaurantName");

            int selectedRestaurantId = restaurants.FirstOrDefault()?.RestaurantId ?? 0;

            List<Zone> zones = _context.Zones
                .Where(z => z.RestaurantId == selectedRestaurantId)
                .ToList();

            ViewBag.Zones = new SelectList(zones, "ZoneId", "ZoneName");

            var reservation = new Reservation();

            return View(reservation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeReservation(Reservation reservation)
        {
            var user = new User
            {
                UserName = reservation.User.UserName,
                UserSurname = reservation.User.UserSurname,
                UserEmail = reservation.User.UserEmail,
                UserPhone = reservation.User.UserPhone
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            reservation.UserId = user.UserId;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ReservationDetails), new { id = reservation.ReservationId });
        }

        public async Task<IActionResult> ReservationDetails(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Restaurant)
                .Include(r => r.Zone)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

    }
}
