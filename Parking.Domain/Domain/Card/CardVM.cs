using Parking.Domain.Domain.Car;
using Parking.Domain.Domain.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Domain.Domain.Card
{
    public class CardVM:BaseDomain
    {
        public decimal Credit { get; set; }
        public int CarId { get; set; }
        public int EmoployeeId { get; set; }
        public CarVM Car { get; set; }
    }
}
