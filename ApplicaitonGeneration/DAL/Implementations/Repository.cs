using ApplicationGeneration.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace ApplicationGeneration.DAL.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            return _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Attach(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            //ObjectStateEntry entry;
            //// Track whether we need to perform an attach
            //bool attach = false;
            //if (
            //    context.ObjectStateManager.TryGetObjectStateEntry
            //        (
            //            context.CreateEntityKey(entitySetName, entity),
            //            out entry
            //        )
            //    )
            //{
            //    // Re-attach if necessary
            //    attach = entry.State == EntityState.Detached;
            //    // Get the discovered entity to the ref
            //    entity = (T)entry.Entity;
            //}
            //else
            //{
            //    // Attach for the first time
            //    attach = true;
            //}
            //if (attach)
            //    context.AttachTo(entitySetName, entity);
        }

        public void RemoveById(int Id)
        {
            var entity = this.Get(Id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}