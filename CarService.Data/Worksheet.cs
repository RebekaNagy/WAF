using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Data
{
    public class Worksheet
    {
        [Key]
        public int Id { get; set; }

        public ICollection<CostToWorksheet> CostToWorksheet { get; set; }

        public string ClientId { get; set; }
        public CarServiceUser Client { get; set; }

        public string MechanicId { get; set; }
        public CarServiceUser Mechanic { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
