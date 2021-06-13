using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebAPIClient.Models;

namespace WebAPIClient.Services
{
    public class VSFlyServices : IVSFlyServices
    {
        private readonly HttpClient _client;
        private readonly string _baseURI;
        public VSFlyServices(HttpClient client)
        {
            _client = client;
            _baseURI = "https://localhost:5003/api/";
        }

        UserM IVSFlyServices.user { get; set; }

        public async Task<IEnumerable<FlightM>> GetAvailableFligths()
        {
            var uri = _baseURI + "Flights/available";

            var responseString = await _client.GetStringAsync(uri);
            var flightsList = JsonConvert.DeserializeObject<IEnumerable<FlightM>>(responseString);

            foreach(FlightM f in flightsList)
            {
                var PriceUri = _baseURI + "Flights/price/"+f.FlightNo;

                var PriceResponseString = await _client.GetStringAsync(PriceUri);
                f.Price = double.Parse(PriceResponseString);
            }

            return flightsList;

        }

        public async Task<IEnumerable<BookingM>> GetSoldTicketsDest(String destination)
        {
            var uri = _baseURI + "Bookings/byDest/"+destination;
            List<PassengerBooking> pbl = new List<PassengerBooking>();
            PassengerBooking pb;

            var responseString = await _client.GetStringAsync(uri);
            var bookingsList = JsonConvert.DeserializeObject<IEnumerable<BookingM>>(responseString);

            foreach (BookingM booking in (List<BookingM>)bookingsList)
            {
                pb = new PassengerBooking();
                pb.FlightNo = booking.FlightNo;
                pb.Price = booking.Price;

                //getPassengers

                pbl.Add(pb);
            }

            return bookingsList;

        }

        public async Task<double> GetAveragePricePerDestination(String destination)
        {

            var AverageUri = _baseURI + "Bookings/avg/" + destination;

            var AverageString = await _client.GetStringAsync(AverageUri);
            double average = double.Parse(AverageString);

            return average;

        }

        public async Task<double> GetFlightRevenue(int FlightNo)
        { 

            var RevenueUri = _baseURI + "Bookings/flightRevenue/" + FlightNo;

            var RevenueString = await _client.GetStringAsync(RevenueUri);
            double average = double.Parse(RevenueString);

            return average;

        }

        public async void PostBooking(BookingM booking)
        {
            

            var postTask = await _client.PostAsJsonAsync<BookingM>("Bookings", booking);

        }

        public async Task<PassengerM> PostPassenger(UserM passenger)
        {
            Uri uri = new Uri(_baseURI + "Passenger");
            _client.BaseAddress = uri;


            PassengerM passengerSend = new PassengerM();
            passengerSend.PassengerID = 0;
            passengerSend.Firstname = passenger.Firstname;
            passengerSend.Lastname = passenger.Lastname;


            var postTask2 = await _client.PostAsJsonAsync<PassengerM>("Passenger", passengerSend).Result.Content.ReadAsStringAsync();

            PassengerM passengerTemp = JsonConvert.DeserializeObject<PassengerM>(postTask2);

            return passengerTemp;
        }
    }
}
