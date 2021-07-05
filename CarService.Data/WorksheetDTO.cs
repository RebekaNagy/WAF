using System;
using System.Collections.Generic;
using System.Text;

namespace CarService.Data
{
    public class WorksheetDTO
    {
        public int ReservationId { get; set; }

        public List<int> CostIds { get; set; }
    }
}
