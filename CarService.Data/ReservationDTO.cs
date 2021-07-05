using System;
using System.Collections.Generic;
using System.Text;

namespace CarService.Data
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string Type { get; set; }

        public string Comment { get; set; }

        public string ClientName { get; set; }

        public ReservationDTO(Reservation reservation)
        {
            Id = reservation.Id;
            Time = reservation.Time;
            Type = reservation.Type.ToString();
            Comment = reservation.Comment;
            ClientName = reservation.Client.Name;
        }

        public ReservationDTO()
        {
            Time = new DateTime();
        }
    }

}
