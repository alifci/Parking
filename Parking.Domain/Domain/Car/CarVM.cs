using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Domain.Domain.Car
{
    public class CarVM:BaseDomain
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
    }
}
