using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public class NHibernateRepository<T> : NHibernateBase, IRepository<T> where T : EntityBase
    {

        public NHibernateRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public void Add(T item)
        {
            Transact(() => Session.Save(item));
        }

        public bool Contains(T item)
        {
            if (item.Id == default(int))
            {
                return false;
            }
            return Transact(() => Session.Get<T>(item.Id)) != null;
        }

        public int Count
        {
            get { return Transact(() => Session.Query<T>().Count()); }
        }

        public bool Remove(T item)
        {
            Transact(() => Session.Delete(item));
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Transact(() => Session.Query<T>().Take(1000).GetEnumerator());
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Transact(() => GetEnumerator());
        }
    }
}
