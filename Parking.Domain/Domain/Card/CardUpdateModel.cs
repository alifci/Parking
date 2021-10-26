using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parking.Domain.Domain.Card
{
    public class CardUpdateModel
    {
        public decimal Credit { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int EmoployeeId { get; set; }
    }
}
