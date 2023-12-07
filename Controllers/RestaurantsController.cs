using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationApp1.Data;
using ReservationApp1.Models;

namespace ReservationApp1.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShowListOfRestaurants()
        {
            var restaurants = await _context.Restaurants
                .Include(r => r.Cuisine)
            .ToListAsync();

            if (restaurants == null)
            {
                return NotFound();
            }
            return View(restaurants);
        }


        public async Task<IActionResult> ShowRestaurantDetails(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Cuisine)
                .Include(r => r.Zones)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }


        public async Task<IActionResult> ShowMenuItems(int cuisineId, int restaurantId)
        {
            var cuisine = await _context.Cuisines
                .Include(c => c.MenuItem)
                .FirstOrDefaultAsync(c => c.CuisineId == cuisineId);

            if (cuisine == null)
            {
                return NotFound();
            }

            ViewBag.RestaurantId = restaurantId;

            return View(cuisine);
        }

        public async Task<IActionResult> ShowReservedDays(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Reservations)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var reservedDays = restaurant.Reservations
                .Select(r => r.ReservationDate.Date)
                .ToList();

            ViewBag.RestaurantId = id;
            ViewBag.ReservedDays = reservedDays;

            return View();
        }
    }
}
