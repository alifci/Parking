using Parking.Entity.Context;
using Parking.Entity.Repository.CardRep;
using Parking.Entity.Repository.CarRep;
using Parking.Entity.Repository.EmployeeRep;
using Parking.Entity.Repository.HighwayGatePassingRep;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Entity.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ParkingContext db)
        {
            this.db = db;
        }

        public ParkingContext db { get; private set; }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }


        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(db);
        public ICarRepository CarRepository => new CarRepository(db);
        public ICardRepository CardRepository => new CardRepository(db);
        public IHighwayGatePassingRepository HighwayGatePassingRepository => new HighwayGatePassingRepository(db);

        
    }
}
