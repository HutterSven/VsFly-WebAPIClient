using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIClient.Models
{
    public class FlightListItemM
    {
        public int FlightNo { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }

        public double TotalRevenue { get; set; }

    }
}
