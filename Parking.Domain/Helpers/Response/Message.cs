using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Domain.Helpers.Response
{
    public class Message
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Error { get; set; }
    }
}
