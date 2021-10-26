using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Domain.Helpers.Response
{
    public class OutputModelOneItem<T>
    {
        public T Item { get; set; }
    }
}
