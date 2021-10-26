using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parking.Entity.Entity
{
    public class HighwayGatePassing : BaseEntity
    {
        public decimal Cost { get; set; }
        public bool IsFree { get; set; }

        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
    }
}
