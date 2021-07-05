using System;
using System.Collections.Generic;
using System.Text;

namespace CarService.Data
{
    public class CostToWorksheet
    {
        public int CostId { get; set; }
        public Cost Cost { get; set; }

        public int WorksheetId { get; set; }
        public Worksheet Worksheet { get; set; }
    }
}
