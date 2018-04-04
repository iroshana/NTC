using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IEntityService<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Update(T entity, List<string> properties, bool isIncluded);
        void Delete(int id);
        void Delete(T entity);
        T GetById(int Id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> where);
        IEnumerable<U> GetAll<U>(Expression<Func<T, bool>> where, Expression<Func<T, U>> columns);
        IEnumerable<T> ExecuteStoredProcedure(string query, params object[] parameters);
    }
}
