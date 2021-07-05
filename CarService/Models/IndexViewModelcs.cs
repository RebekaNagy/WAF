using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Data;

namespace CarService.Models
{
    public class IndexViewModel
    {
        public CarServiceUser User;
        public Dictionary<CarServiceUser, Calendar> Calendars = new Dictionary<CarServiceUser, Calendar>();
    }

    public class Calendar
    {
        public CarServiceUser Mechanic;
        public Dictionary<DayOfWeek, ReservationWithStatus[]> Reservations = new Dictionary<DayOfWeek, ReservationWithStatus[]>();

        public Calendar(CarServiceUser mechanic)
        {
            this.Mechanic = mechanic;

            for (var i = 0; i < 7; i++)
            {
                Reservations[(DayOfWeek)i] = new ReservationWithStatus[8];
                for (var j = 0; j < 8; j++)
                {
                    Reservations[(DayOfWeek)i][j] = new ReservationWithStatus();
                }
            }
        }
    }

    public class ReservationWithStatus
    {
        public Reservation Reservation;
        public ReservationStatus Status;
    }

    public enum ReservationStatus
    {
        Free,
        Elapsed,
        Occupied,
        Owned
    }
}
