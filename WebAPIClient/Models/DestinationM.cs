using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIClient.Models
{
    public class DestinationM
    {
        public string Destination { get; set; }

        public double avgPrice { get; set; }

        public List<PassengerBooking> bookings{ get; set; }

        
    }
}
