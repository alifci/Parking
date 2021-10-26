using Microsoft.EntityFrameworkCore;
using Parking.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Entity.Repository
{
    public class Repository<Entity, PrimaryKey> : IRepository<Entity, PrimaryKey>
                where Entity : class
    {
        private ParkingContext _db { get; set; }

        public Repository(ParkingContext db)
        {
            _db = db;
        }



        public async Task<Entity> Get(PrimaryKey key)
        {
            var entity = await _db.Set<Entity>().FindAsync(key);
            return entity;
        }

        public IQueryable<Entity> Get(Expression<Func<Entity, bool>> expression = null, string includes = "")
        {

            IQueryable<Entity> query = _db.Set<Entity>();

            if (!string.IsNullOrWhiteSpace(includes))
                query = includes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, include) => current.Include(include));

            if (expression == null)
                return query;
            else
                return query.Where(expression);
        }

        public async Task<bool> Check(Expression<Func<Entity, bool>> predicate = null)
        {
            IQueryable<Entity> query = _db.Set<Entity>();

            if (predicate == null)
                return await query.AnyAsync();
            else
                return await query.AnyAsync(predicate);
        }
        public async Task<Entity> Create(Entity entity)
        {

            if (entity == null)
                throw new ArgumentNullException();

            var addedEntity = await _db.Set<Entity>().AddAsync(entity);

            return addedEntity.Entity;
        }

        public async Task<IEnumerable<Entity>> Create(IEnumerable<Entity> entities)
        {
            await _db.Set<Entity>().AddRangeAsync(entities);

            return entities;
        }


        public Entity Update(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var addedEntity = _db.Set<Entity>().Update(entity).Entity;

            return addedEntity;
        }

        public IEnumerable<Entity> Update(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException();

            _db.Set<Entity>().UpdateRange(entities);

            return entities;
        }


        public void Delete(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            _db.Set<Entity>().Remove(entity);
        }

        public void Delete(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException();

            _db.Set<Entity>().RemoveRange(entities);
        }


        public void Delete(PrimaryKey key)
        {
            var entity = _db.Set<Entity>().Find(key);
            if (entity == null)
                throw new Exception("Not found");
            _db.Set<Entity>().Remove(entity);
        }
    }
}
