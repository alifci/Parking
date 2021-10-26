using Microsoft.EntityFrameworkCore;
using Parking.Entity.Context;
using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Entity.Repository.EmployeeRep
{
    public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
    {
        private ParkingContext _db { get; set; }

        public EmployeeRepository(ParkingContext db)
            : base(db)
        {
            _db = db;
        }

    }
}
