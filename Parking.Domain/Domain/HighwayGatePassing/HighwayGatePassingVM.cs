﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Domain.Domain.HighwayGatePassing
{
    public class HighwayGatePassingVM:BaseDomain
    {
        public int CarId { get; set; }
        public decimal Cost { get; set; }
        public bool IsFree { get; set; }
    }
}
