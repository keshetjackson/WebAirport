using ConsoleSimulator.Models;
using ConsoleSimulator.Services;
using RandomFriendlyNameGenerator;
using System.Net.Http.Json;

SimulatorService _service = new SimulatorService("https://localhost:7275");

System.Timers.Timer timer = new System.Timers.Timer(5000);
timer.Elapsed += (s, e) => _service.GenerateFlight();

Thread.Sleep(15000);
timer.Start();

Console.ReadLine();


