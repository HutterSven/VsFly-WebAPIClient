using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIClient.Models
{
    public class PassengerBooking
    {

        public int FlightNo { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public double Price { get; set; }


    }
}
