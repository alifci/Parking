using AutoMapper;
using Parking.Domain.Domain.Car;
using Parking.Domain.Domain.Card;
using Parking.Domain.Domain.Employee;
using Parking.Domain.Domain.HighwayGatePassing;
using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<EmployeeModel, Employee>();
            CreateMap<Employee, EmployeeVM>();

            CreateMap<CarModel, Car>();
            CreateMap<Car, CarVM>();

            CreateMap<CardModel, Card>();
            CreateMap<CardUpdateModel, Card>();
            CreateMap<Card, CardVM>();

            CreateMap<HighwayGatePassingModel, HighwayGatePassing>();
            CreateMap<HighwayGatePassing, HighwayGatePassingVM>();

            CreateMissingTypeMaps = true;
        }

    }
}
