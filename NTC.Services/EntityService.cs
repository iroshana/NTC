using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTC.Services
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        IUnitOfWork _unitOfWork;
        IRepository<T> _repository;

        public EntityService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _repository.Update(entity);
            _unitOfWork.Commit();
        }

        public virtual void Update(T entity, List<string> properties, bool isIncluded)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (!isIncluded)
            {
                properties.Add("OutletID");
                properties.Add("CreatedBy");
                properties.Add("CreatedDate");
            }

            _repository.Update(entity, properties, isIncluded);
            _unitOfWork.Commit();
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
            _unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public virtual T GetById(int Id)
        {
            return _repository.GetById(Id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.Get();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return _repository.Get(where);
        }

        public virtual IEnumerable<U> GetAll<U>(Expression<Func<T, bool>> where, Expression<Func<T, U>> columns)
        {
            return _repository.Get<U>(where, columns);
        }

        public virtual IEnumerable<T> ExecuteStoredProcedure(string query, params object[] parameters)
        {
            return _repository.ExecuteStoredProcedure(query, parameters);
        }
    }
}
