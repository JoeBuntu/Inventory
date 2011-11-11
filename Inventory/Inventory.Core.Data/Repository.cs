using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : EntityBase
    {
        public Repository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public int Count
        {
            get
            {
                return base.Transact<int>(() => Session.Query<T>().Count());
            }
        }

        public T Get(int id)
        {
            return this.Transact<T>(() => Session.Get<T>(id));
        }

        public List<T> Get(int start_index, int count)
        {
            return this.Transact<List<T>>(
                    () => Session.Query<T>().Skip(start_index).Take(count).ToList()
                );
        }

        public IList<T> Get(int start_index, int count, string sort_column, bool sort_asc)
        {
            return this.Transact<IList<T>>(
                    () => Session.CreateCriteria<T>()
                                 .AddOrder(new NHibernate.Criterion.Order(sort_column, sort_asc))
                                 .SetFirstResult(start_index)
                                 .SetMaxResults(count)
                                 .List<T>()
                );
        }

        public int Add(T item)
        {
            try
            {
                return this.Transact<int>(() => (int)Session.Save(item));
            }
            catch(Exception ex)
            {
                OnAddException(item, ex);
                throw ex;
            }
        }

        protected virtual void OnAddException(T item, Exception ex)
        {
            throw ex;
        }

        public virtual void Update(T item)
        {
            try 
            {
                this.Transact(() => Session.Update(item));
            }
            catch (Exception ex)
            {
                OnAddException(item, ex);
                throw ex;
            }
        }

        protected virtual void OnUpdateException(T item, Exception ex)
        {
            throw ex;
        }


        public void Delete(int id)
        {
            T item = Get(id);
            this.Delete(item);
        }

        public void Delete(T item)
        {
            try
            {
                this.Transact(() => Session.Delete(item));
            }
            catch (Exception ex)
            {
                OnAddException(item, ex);
                throw ex;
            }
        }

        protected virtual void OnDeleteException(T item, Exception ex)
        {
            throw ex;
        }
    }
}
