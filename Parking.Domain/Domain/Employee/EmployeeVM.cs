using Parking.Domain.Domain.Car;
using Parking.Domain.Domain.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Domain.Domain.Employee
{
    public class EmployeeVM : BaseDomain
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public CardVM Card { get; set; }
    }
}
