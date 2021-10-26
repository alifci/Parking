using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Entity.Repository
{
    public interface IRepository<Entity, PrimaryKey>
                where Entity : class
    {
        Task<Entity> Get(PrimaryKey key);
        IQueryable<Entity> Get(Expression<Func<Entity, bool>> expression = null, string includes = "");
        Task<bool> Check(Expression<Func<Entity, bool>> predicate = null);
        Task<Entity> Create(Entity entity);
        Task<IEnumerable<Entity>> Create(IEnumerable<Entity> entities);
        IEnumerable<Entity> Update(IEnumerable<Entity> entities);
        Entity Update(Entity entity);
        void Delete(Entity entity);
        void Delete(IEnumerable<Entity> entities);
        void Delete(PrimaryKey key);

    }
}
