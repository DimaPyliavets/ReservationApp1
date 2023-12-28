using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationApp1.Data;
using ReservationApp1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            PopulateDropdowns();
            var reservation = new Reservation();

            return View(reservation);
        }

        [HttpGet]
        public JsonResult GetZones(int restaurantId)
        {
            var zones = _context.Zones
                .Where(z => z.RestaurantId == restaurantId)
                .Select(z => new { value = z.ZoneId, text = z.ZoneName })
                .ToList();

            return Json(zones);
        }

        [HttpGet]
        public JsonResult GetTables(int restaurantId)
        {
            var tables = _context.Tables
                .Where(z => z.RestaurantId == restaurantId)
                .Select(z => new { value = z.TableId })
                .ToList();

            return Json(tables);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeReservation(Reservation reservation)
        {
            //if (!ModelState.IsValid)
            //{
            //    PopulateDropdowns(reservation.RestaurantId);
            //    return View(reservation);
            //}

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

            List<Table> tables = new List<Table>();

            //foreach (var table in _context.Tables) 
            //{
            //    if (table) 
            //    {
            //        tables.Add(table);
            //    }
            //}

            //reservation.Tables = _context.Tables.Where(t => reservation.Tables.Contains(t.TableId)).ToList();
            reservation.Zone = _context.Zones.FirstOrDefault(z => z.ZoneId == reservation.ZoneId);
            reservation.Restaurant = _context.Restaurants.FirstOrDefault(r => r.RestaurantId == reservation.RestaurantId);

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ReservationDetails), new { id = reservation.ReservationId });
        }


        private bool IsDateAvailable(DateTime reservationDate, int restaurantId)
        {
            return !_context.Reservations.Any(r =>
                r.ReservationDate.Date == reservationDate.Date &&
                r.RestaurantId == restaurantId);
        }

        private void PopulateDropdowns(int selectedRestaurantId = 0)
        {
            List<Restaurant> restaurants = _context.Restaurants.ToList();
            ViewBag.Restaurants = new SelectList(restaurants, "RestaurantId", "RestaurantName", selectedRestaurantId);

            List<Zone> zones = _context.Zones
                .Where(z => z.RestaurantId == selectedRestaurantId)
                .ToList();

            List<Table> tables = _context.Tables
                .Where(z => z.RestaurantId == selectedRestaurantId)
                .ToList();

            ViewBag.Zones = new SelectList(zones, "ZoneId", "ZoneName");
            ViewBag.Tables = new SelectList(tables, "TableId");

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

        public async Task<IActionResult> ShowReservedDays(int restaurantId)
        {
            // Get the reserved days for the specified restaurant
            var reservedDays = await _context.Reservations
                .Where(r => r.RestaurantId == restaurantId)
                .Select(r => r.ReservationDate.Date)
                .Distinct()
                .ToListAsync();

            // Pass the reserved days to the view
            return View(reservedDays);
        }

        [HttpGet]
        public IActionResult GetZoneCount(int restaurantId)
        {
            var zoneCount = _context.Zones.Count(z => z.RestaurantId == restaurantId);
            return Json(zoneCount);
        }

        [HttpGet]
        public IActionResult GetTableCount(int restaurantId)
        {
            var tableCount = _context.Tables.Count(t => t.RestaurantId == restaurantId);
            return Json(tableCount);
        }

        [HttpGet]
        public JsonResult GetReservedTables(int restaurantId, DateTime reservationDate)
        {
            var reservedTableIds = _context.Reservations
                 .Where(r =>
                     r.RestaurantId == restaurantId &&
                     r.ReservationDate.Date == reservationDate.Date)
                 .SelectMany(r => r.Tables.Select(t => t.TableId))
                 .ToList();

            return Json(reservedTableIds);
        }

    }
}
