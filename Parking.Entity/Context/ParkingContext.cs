using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Entity.Context
{
    public partial class ParkingContext : DbContext
    {

        public ParkingContext(DbContextOptions<ParkingContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<HighwayGatePassing> HighwayGatePassinges { get; set; }
        

        //lazy-loading
        //https://entityframeworkcore.com/querying-data-loading-eager-lazy
        //https://docs.microsoft.com/en-us/ef/core/querying/related-data
        //EF Core will enable lazy-loading for any navigation property that is virtual and in an entity class that can be inherited


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API           

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.Now;
                }
            ((BaseEntity)entry.Entity).Modified = DateTime.Now;
            }
        }

    }
}
