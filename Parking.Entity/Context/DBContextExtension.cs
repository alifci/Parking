using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Parking.Entity.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using Parking.Entity.Entity;
using Microsoft.AspNetCore.Identity;

namespace Parking.Entity.Context
{
    public static class DbContextExtension
    {

        public static void EnsureMigrated(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId).ToList();

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key).ToList();
            var test = total.Except(applied).Any();
            if (total.Except(applied).Any())
                context.Database.Migrate();
        }

        public static void EnsureSeeded(this ParkingContext context)
        {
            var entityEmp = new Employee();
            var entityCar = new Car();

            //Ensure we have some status
            if (!context.Employees.Any())
            {
                var employee = JsonConvert.DeserializeObject<Employee>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "employees.json"));
                entityEmp = context.Add(employee).Entity;
                context.SaveChanges();
            }

            if (!context.Cars.Any())
            {
                var car = JsonConvert.DeserializeObject<Car>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "cars.json"));
                entityCar = context.Add(car).Entity;
                context.SaveChanges();
            }

            if (!context.Cards.Any())
            {
                var card = JsonConvert.DeserializeObject<Card>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "cards.json"));
                card.EmoployeeId = entityEmp.Id;
                card.CarId = entityCar.Id;
                context.Add(card);
                context.SaveChanges();
            }
        }


    }
}
