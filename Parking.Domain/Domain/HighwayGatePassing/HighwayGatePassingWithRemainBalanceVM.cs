using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Domain.Domain.HighwayGatePassing
{
    public class HighwayGatePassingWithRemainBalanceVM : BaseDomain
    {
        public int CarId { get; set; }
        public decimal Cost { get; set; }
        public bool IsFree { get; set; }
        public decimal CardRemainingBalance { get; set; }
    }
}
