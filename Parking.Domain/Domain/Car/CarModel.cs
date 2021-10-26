using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parking.Domain.Domain.Car
{
    public class CarModel
    {
        [Required]
        [StringLength(maximumLength:200)]
        public string Brand { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Model { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string PlateNumber { get; set; }
    }
}
