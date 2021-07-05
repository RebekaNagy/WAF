using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarService.Data;
using Microsoft.AspNetCore.Mvc;
using CarService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CarService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<CarServiceUser> userManager;

        public HomeController(ApplicationDbContext context, UserManager<CarServiceUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Mechanic"))
            {
                return View("MechanicApp");
            }

            var viewModel = new IndexViewModel();
            var mechanics = await userManager.GetUsersInRoleAsync("Mechanic");
            var relevantReservations =
                context.Reservations.Where(r => r.Time.Day >= DateTime.Now.Day && r.Time.Day < DateTime.Now.Day + 7);

            viewModel.User = await userManager.GetUserAsync(HttpContext.User);

            foreach (var mechanic in mechanics)
            {
                viewModel.Calendars.Add(mechanic, new Calendar(mechanic));
            }

            foreach (var reservation in relevantReservations)
            {
                viewModel.Calendars[reservation.Mechanic]
                    .Reservations[reservation.Time.DayOfWeek][reservation.Time.Hour - 9] = new ReservationWithStatus()
                {
                    Reservation = reservation,
                    Status = reservation.ClientId == viewModel.User.Id
                        ? ReservationStatus.Owned
                        : ReservationStatus.Occupied
                };
            }

            var currentHourIndex = Math.Min(DateTime.Now.Hour - 9, 7);

            foreach (var calendar in viewModel.Calendars.Values)
            {
                for (int i = 0; i <= currentHourIndex; i++)
                {
                    calendar.Reservations[DateTime.Now.DayOfWeek][i].Status = ReservationStatus.Elapsed;
                }
            }

            return View(viewModel);
        }

        /*
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        */

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
