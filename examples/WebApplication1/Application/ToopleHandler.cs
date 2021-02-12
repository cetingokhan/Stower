using Stower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Domain;

namespace WebApplication1.Application
{
    public class ToppleHandler : IToppleHandler
    {
        public async Task Handle(List<object> objects)
        {
            Thread.Sleep(5000);
            foreach (var item in objects)
            {
                if (item is WeatherData)
                {
                    var value = (WeatherData)Convert.ChangeType(item, typeof(WeatherData));

                    Console.WriteLine($"{value.Location} -> {value.Degree}");
                }
            }
            await Task.CompletedTask;
        }

    }
}
