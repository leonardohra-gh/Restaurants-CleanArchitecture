using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Model
{
    public class TemperatureRequest
    {
        public int MinimumTemperature { get; set; }
        public int MaximumTemperature { get; set; }
    }
}