using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarService.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class CarServiceUser : IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Reservation> ReservationsOfClient { get; set; }
        public ICollection<Reservation> ReservationsOfMechanic { get; set; }
        public ICollection<Worksheet> WorksheetsOfClient { get; set; }
        public ICollection<Worksheet> WorksheetsOfMechanic { get; set; }
        public byte IsMechanic { get; set; }
    }
}
