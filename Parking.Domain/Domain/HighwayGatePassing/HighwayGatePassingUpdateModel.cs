using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parking.Domain.Domain.HighwayGatePassing
{
    public class HighwayGatePassingUpdateModel
    {
        public int CarId { get; set; }
        public decimal Cost { get; set; }
        public bool IsFree { get; set; }
    }
}
