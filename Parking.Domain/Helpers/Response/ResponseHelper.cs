using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Domain.Helpers.Response
{
    public class ResponseHelper<T>
    {
        public T Data { get; set; }
        public Message Message { get; set; }
    }
}
