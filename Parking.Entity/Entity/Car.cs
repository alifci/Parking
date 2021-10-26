using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parking.Entity.Entity
{
    public class Car : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Brand { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Model { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string PlateNumber { get; set; }

        public Card Card { get; set; }
        public List<HighwayGatePassing> HighwayGatePassinges { get; set; }

    }
}
