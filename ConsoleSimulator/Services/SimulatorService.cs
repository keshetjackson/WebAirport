using ConsoleSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSimulator.Services
{
    internal class SimulatorService
    {
        public SimulatorService(string clientAdress)
        {
            client = new() { BaseAddress = new Uri(clientAdress) };
        }
        HttpClient client ;
        public async void GenerateFlight()
        {
            var flight = new FlightDto { Company = GenerateCompany(), IsDeparture = GenerateDeparture(), IsCritical = GenerateIsCritical()};
            var response = await client.PostAsJsonAsync("api/Airport", flight);
            if (response.IsSuccessStatusCode)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(flight.IsDeparture ? $"flight: {flight.Code}, company: {flight.Company}, is Depurted" :
                    $"flight: {flight.Code}, company: {flight.Company}, is Landing");
                builder.Append(flight.IsCritical ? "and is critical!" : "and is not critical");
                Console.WriteLine(builder.ToString());
            }              
        }

         string GenerateCompany()
        {
            var company = (Ecompanies)new Random().Next(0, 5);
            return company.ToString();
        }

         bool GenerateDeparture()
        {
            return Convert.ToBoolean(new Random().Next(0, 2)); 
        }

        bool GenerateIsCritical()
        {
            return Convert.ToBoolean(new Random().Next(0, 2));
        }
       
    }
    public enum Ecompanies
    {
        Arkia,
        Israir,
        ElAl,
        WizAir,
        Ryanair,
    }
}
