using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parking.Entity.Entity
{
    public class Card :BaseEntity
    {
        public decimal Credit { get; set; }
        public int EmoployeeId { get; set; }
        [ForeignKey("EmoployeeId")]
        public Employee Employee { get; set; }
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
    }
}
