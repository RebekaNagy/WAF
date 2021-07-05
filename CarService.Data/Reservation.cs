using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarService.Data
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public ReservationType Type { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public string ClientId { get; set; }
        public CarServiceUser Client { get; set; }

        public string MechanicId { get; set; }
        public CarServiceUser Mechanic { get; set; }

        public int? WorksheetId { get; set; }
        public Worksheet Worksheet { get; set; }

    }

    public enum ReservationType
    {
        MandatoryService,
        TechnicalExamination,
        Malfunction
    }

}
