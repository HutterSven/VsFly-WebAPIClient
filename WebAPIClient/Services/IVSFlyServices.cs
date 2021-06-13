﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIClient.Models;

namespace WebAPIClient.Services
{
    public interface IVSFlyServices
    {
        public UserM user { get; set; }
        public Task<IEnumerable<FlightM>> GetAvailableFligths();

        public void PostBooking(BookingM booking);
        public void PostPassenger(UserM passenger);

        public Task<IEnumerable<BookingM>> GetSoldTicketsDest(String destination);

        public Task<double> GetAveragePricePerDestination(String destination);

        public Task<double> GetFlightRevenue(int FlightNo);





    }

}