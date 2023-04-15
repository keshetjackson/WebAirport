using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;
using WebTerminalsServer.Services;

namespace WebAirport.Server.Tests
{
    public class AirportServiceTests
    {
        private readonly Mock<IAirPortRepository> _repository;
        private readonly AirportService _service;
        public AirportServiceTests()
        {
            _repository = new Mock<IAirPortRepository>();
            _service = new AirportService(_repository.Object);
        }

        [Fact]
        public async Task ProcessFlight_ValidFlight_AddsLog()
        {
            //arrange
            Flight flight = new Flight {Id = 1, Code = "adasda", Company = "elal" , IsCritical = true, IsDeparture = false };
            _repository.Setup(repo => repo.AddFlightAsync(It.IsAny<Flight>())).Returns(Task.CompletedTask);

            //act 
            _service.ProccessFlight(flight);

            //assert
            _repository.Verify(repo => repo.AddFlightAsync(It.Is<Flight>(f =>           
                f.Id == flight.Id &&
                f.Code == flight.Code &&
                f.Company == flight.Company &&
                f.IsCritical == flight.IsCritical &&
                f.IsDeparture == flight.IsDeparture
            )), Times.Once);
        }
    }
}
