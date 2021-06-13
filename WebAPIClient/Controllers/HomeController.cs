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
        UserM user;

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
            _vsFly.user = user;
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
            booking.PassengerID = 1;
            booking.Price = flight.Price;

            UserM user = new UserM();
            user.Firstname = "Peter";
            user.Lastname = "PenisHodensack";

            _vsFly.PostBooking(booking);

            _vsFly.PostPassenger(user);
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
