using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private NTCEntities _dbContext;
        private readonly IDbSet<T> _dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        public NTCEntities DbContext
        {
            get { return _dbContext ?? (_dbContext = DbFactory.Init()); }
        }
        public RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(T entity, List<string> properties, bool isIncluded)
        {
            _dbSet.Attach(entity);

            if (!isIncluded)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }

            if (properties != null && properties.Count > 0)
            {
                var attachedEntity = DbContext.Entry(entity);
                foreach (var name in properties)
                {
                    var property = attachedEntity.Property(name);
                    if (property != null)
                    {
                        attachedEntity.Property(name).IsModified = isIncluded;
                    }
                }
            }
        }

        public virtual void Delete(object id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> entities = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T entity in entities)
                _dbSet.Remove(entity);
            // muktiple delete removeall()
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual T GetById(int Id)
        {
            T entity = _dbSet.Find(Id);
            return entity;
        }

        public virtual IEnumerable<T> Get()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> entities = _dbSet.Where<T>(where).AsEnumerable();
            return entities;
        }

        private System.Data.Entity.Core.Objects.ObjectQuery<T> GetQueryFromQueryable<T>(IQueryable<T> query)
        {
            var internalQueryField = query.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(f => f.Name.Equals("_internalQuery")).FirstOrDefault();
            var internalQuery = internalQueryField.GetValue(query);
            var objectQueryField = internalQuery.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(f => f.Name.Equals("_objectQuery")).FirstOrDefault();
            return objectQueryField.GetValue(internalQuery) as System.Data.Entity.Core.Objects.ObjectQuery<T>;
        }

        public virtual IEnumerable<U> Get<U>(Expression<Func<T, bool>> where, Expression<Func<T, U>> columns)
        {
            IEnumerable<U> entities = _dbSet.Where<T>(where).Select<T, U>(columns).AsEnumerable();
            return entities;
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> where, params string[] children)
        {
            var query = _dbSet.AsQueryable<T>();
            foreach (var child in children)
            {
                query = query.Include(child);
            }
            IEnumerable<T> entities = query.Where<T>(where).AsEnumerable();
            return entities;
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>>
            includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => query.Include(i));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);


            return query.ToList();
        }

        public virtual IEnumerable<T> ExecuteStoredProcedure(string query, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<T>(String.Format("EXEC {0}", query), parameters);
        }
    }
}
