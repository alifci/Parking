using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Parking.Entity.Repository.CardRep
{
    public interface ICardRepository : IRepository<Card, int>
    {
    }
}
