using Microsoft.EntityFrameworkCore;
using Parking.Entity.Context;
using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Entity.Repository.HighwayGatePassingRep
{
    public class HighwayGatePassingRepository : Repository<HighwayGatePassing, int>, IHighwayGatePassingRepository
    {
        private ParkingContext _db { get; set; }

        public HighwayGatePassingRepository(ParkingContext db)
            : base(db)
        {
            _db = db;
        }

    }
}
