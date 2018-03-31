using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public interface IRepository<T> where T : class
    {
        NTCEntities DbContext { get; }
        void Add(T entity);
        void Update(T entity);
        void Update(T entity, List<string> properties, bool isIncluded);
        void Delete(object id);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int Id);
        //T GetByName(string Name);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> where);
        IEnumerable<U> Get<U>(Expression<Func<T, bool>> where, Expression<Func<T, U>> columns);
        IEnumerable<T> Get(Expression<Func<T, bool>> where, params string[] children);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>>
            includeProperties = null,
            int? page = null,
            int? pageSize = null);
        IEnumerable<T> ExecuteStoredProcedure(string query, params object[] parameters);
    }
}
