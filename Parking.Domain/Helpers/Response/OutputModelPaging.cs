using Parking.Domain.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Domain.Helpers.Response
{
    public class OutputModelPaging<T>
    {
        public PagingHeader Paging { get; set; }
        public List<T> Items { get; set; }
    }
}
