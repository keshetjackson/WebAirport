using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTerminalsServer.Controllers;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;
using WebTerminalsServer.Services;

namespace WebAirport.Server.Tests
{
    public class AirportControllerTests
    {
        private readonly Mock<IAirPortRepository> _airportRepository;
        private readonly Mock<IAirportService> _airportService;
        private readonly AirportController _airportController;

        public AirportControllerTests()
        {
            _airportRepository = new Mock<IAirPortRepository>();
            _airportService = new Mock<IAirportService>();
            _airportController = new AirportController(_airportService.Object, _airportRepository.Object);
        }

        [Fact]
        public async Task GetLogs_NoLogsAvailable_ReturnsEmptyList()
        {
            // Arrange
            _airportRepository.Setup(service => service.GetLogs())
                .ReturnsAsync(new List<Logger>());

            // Act
            var result = await _airportController.GetLogs();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Logger>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetLogs_LogsAvailable_ReturnsLogList()
        {
            // Arrange
            var logs = new List<Logger>
             {
                new Logger { Id = 1,EventTime = DateTime.Now, IsEntering = true},
                new Logger { Id = 2,EventTime = DateTime.Now, IsEntering = false}
             };
            _airportRepository.Setup(s => s.GetLogs()).ReturnsAsync(logs);

            // Act
            var result = await _airportController.GetLogs();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Logger>>(result);
            Assert.Equal(logs.Count, result.Count);
        }

        [Fact]
        public async Task GetLegs_NoLegsAvailable_ReturnsEmptyList()
        {
            // Arrange
            _airportRepository.Setup(service => service.GetLegModels())
                .ReturnsAsync(new List<LegModel>());

            // Act
            var result = await _airportController.GetLegs();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<LegModel>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetLegs_LegsAvailable_ReturnsLegList()
        {
            // Arrange
            var legs = new List<LegModel>
            {
                new LegModel { Id = 1, Number = 1, NextLeg = LegType.Two},
                new LegModel { Id = 2, Number = 2, NextLeg = LegType.Three}
            };
           _airportRepository.Setup(s => s.GetLegModels()).ReturnsAsync(legs);

            // Act
            var result = await _airportController.GetLegs();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<LegModel>>(result);
            Assert.Equal(legs.Count, result.Count);
        }




    }
}
