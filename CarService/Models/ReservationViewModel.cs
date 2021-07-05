using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Data;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string MechanicId { get; set; }

        [Display(Name = "Date/Time")]
        public DateTime DateTime { get; set; }
        public ReservationType Type { get; set; }
        public string Comment { get; set; }
    }
}
