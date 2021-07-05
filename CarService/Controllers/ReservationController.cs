using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Data;
using CarService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CarService.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<CarServiceUser> userManager;
        private readonly ApplicationDbContext context;

        public ReservationController(UserManager<CarServiceUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [Authorize]
        public ActionResult Create(string mechanicId, int day, int hour)
        {
            var dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(day).Day, hour + 9, 0, 0);

            var viewModel = new ReservationViewModel()
            {
                ClientId = userManager.GetUserId(HttpContext.User),
                MechanicId = mechanicId,
                DateTime = dateTime
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            var reservation = new Reservation
            {
                ClientId = viewModel.ClientId,
                Comment = viewModel.Comment,
                MechanicId = viewModel.MechanicId,
                Time = viewModel.DateTime,
                Type = viewModel.Type
            };

            if (context.Reservations.Any(r => r.Time == reservation.Time && r.MechanicId == reservation.MechanicId))
            {
                return RedirectToAction("Index", "Home");
            }

            context.Reservations.Add(reservation);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var reservation = context.Reservations.Find(id);
            if (reservation == null || reservation.ClientId != userManager.GetUserId(HttpContext.User))
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new ReservationViewModel()
            {
                Id = reservation.Id,
                DateTime = reservation.Time,
                Comment = reservation.Comment,
                Type = reservation.Type
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var reservation = context.Reservations.Find(id);
            if (!ModelState.IsValid || reservation == null || reservation.ClientId != userManager.GetUserId(HttpContext.User))
            {
                return RedirectToAction("Index", "Home");
            }

            context.Reservations.Remove(reservation);

            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}