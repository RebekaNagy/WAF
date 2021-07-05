using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CarService.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CarServiceUser> _usermanager;

        public HomeController(ApplicationDbContext context, UserManager<CarServiceUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: api/Costs
        [HttpGet]
        public IEnumerable<Cost> GetCosts()
        {
            return _context.Costs;
        }

        [HttpGet("{mechanicUserName}")]
        public IEnumerable<ReservationDTO> GetReservationsByMechanic([FromRoute] string mechanicUserName)
        {
            var mechanic = _context.Users.Include(u => u.ReservationsOfMechanic).ThenInclude(r => r.Client)
                .Include(u => u.ReservationsOfMechanic).ThenInclude(r => r.Worksheet)
                .FirstOrDefault(m => m.UserName == mechanicUserName);
            var reservations = new List<ReservationDTO>();
            foreach (Reservation reservation in mechanic.ReservationsOfMechanic.Where(r => r.Worksheet == null && r.Time > DateTime.Now))
            {
                reservations.Add(new ReservationDTO(reservation));
            }

            return reservations;
        }

        [HttpPost]
        public async Task<IActionResult> PostWorksheet([FromBody] WorksheetDTO worksheetDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var reservation = _context.Reservations.Find(worksheetDTO.ReservationId);

                var actmechanic = await _usermanager.FindByNameAsync(User.Identity.Name);

                if(actmechanic.Id != reservation.MechanicId)
                {
                    return BadRequest();
                }

                Worksheet worksheet = new Worksheet
                {
                    Reservation = reservation,
                    CostToWorksheet = new List<CostToWorksheet>()
                };
                foreach (var costId in worksheetDTO.CostIds)
                {
                    var cost = _context.Costs.Find(costId);
                    var costToWorksheet = new CostToWorksheet
                    {
                        Cost = cost,
                        Worksheet = worksheet
                    };
                    worksheet.CostToWorksheet.Add(costToWorksheet);
                }

                reservation.Worksheet = worksheet;

                _context.Reservations.Update(reservation);

                _context.Worksheets.Add(worksheet);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return NoContent();
        }
    }
}