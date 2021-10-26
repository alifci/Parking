using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parking.Domain.Domain.HighwayGatePassing
{
    public class HighwayGatePassingModel
    {
        [Required]
        public int CarId { get; set; }
    }
}
