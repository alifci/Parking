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
    public interface IUnitOfWork : IDisposable
    {
        
        IEmployeeRepository EmployeeRepository { get; }
        ICarRepository CarRepository { get; }
        ICardRepository CardRepository { get; }
        IHighwayGatePassingRepository HighwayGatePassingRepository { get; }
        

        ParkingContext db { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
