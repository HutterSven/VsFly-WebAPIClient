using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPIClient.Models;
using WebAPIClient.Services;

namespace WebAPIClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVSFlyServices _vsFly;
        List<FlightM> listofFlights;
        List<FlightM> flights;
        BookingM booking;
        public static UserM userTemp;

        public HomeController(ILogger<HomeController> logger, IVSFlyServices vsFly)
        {
            _logger = logger;
            _vsFly = vsFly;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> FlightList(UserM user) 
        {
            userTemp = user;
            //HttpClient client = new HttpClient();
            //string baseURI = "https://localhost:44322";

            //VSFlyClient vsfly = new VSFlyClient(baseURI, client);
            //var listofFligths2 = await vsfly.FlightsAllAsync();

            listofFlights = (List<FlightM>)await _vsFly.GetAvailableFligths();

            flights = new List<FlightM>();

            foreach(FlightM f in listofFlights)
            {
                if(f.Date.Year == user.Date.Year && f.Date.Month == user.Date.Month && f.Date.Day== user.Date.Day && f.Destination == user.Destination)
                {
                    flights.Add(f);
                }
            }

            return View(flights);
        }



        public IActionResult Book(FlightM flight)
        {
            booking = new BookingM();

            booking.FlightNo = flight.FlightNo;
            
            booking.Price = flight.Price;

            int passengerID = _vsFly.PostPassenger(userTemp).Result.PassengerID;

            booking.PassengerID = passengerID;

            _vsFly.PostBooking(booking);

            
            FlightM flight2 = flight;

            return View(flight2);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
