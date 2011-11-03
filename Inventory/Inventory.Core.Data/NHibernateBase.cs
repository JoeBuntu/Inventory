using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Inventory.Core.Data
{
    public class NHibernateBase
    {
        protected readonly ISessionFactory _sessionFactory;
        protected virtual ISession Session
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        public NHibernateBase(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        protected virtual TResult Transact<TResult>(Func<TResult> func)
        {
            if (!Session.Transaction.IsActive)
            {
                // wrap in transaction
                TResult result;
                using (ITransaction tx = Session.BeginTransaction())
                {
                    result = func.Invoke();
                    tx.Commit();
                }
                return result;
            }

            // Don't wrap
            return func.Invoke();
        }

        protected virtual void Transact(Action action)
        {
            Transact<bool>(() =>
            {
                action.Invoke();
                return false;
            });
        }
    }
}
