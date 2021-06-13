using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIClient.Models;

namespace WebAPIClient.Services
{
    public interface IVSFlyServices
    {
        public UserM User { get; set; }
        public Task<IEnumerable<FlightM>> GetAvailableFligths();

        public void PostBooking(BookingM booking);
        public Task<PassengerM> PostPassenger(UserM passenger);

        public Task<List<PassengerBooking>> GetSoldTicketsDest(String destination);

        public Task<double> GetAveragePricePerDestination(String destination);

        public Task<double> GetFlightRevenue(int FlightNo);





    }

}
