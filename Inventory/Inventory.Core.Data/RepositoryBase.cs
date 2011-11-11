using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Inventory.Core.Data
{
    public class RepositoryBase
    {
        ISessionFactory m_SessionFactory;
        ISession m_Session;

        public RepositoryBase(ISessionFactory sessionFactory)
        {
            m_SessionFactory = sessionFactory;
            m_Session = m_SessionFactory.OpenSession();
        }

        protected ISession Session
        {
            get { return m_Session; }
        }

        protected virtual TResult Transact<TResult>(Func<TResult> func)
        {
            TResult result;
            using (ITransaction tx = m_Session.BeginTransaction())
            {
                try
                {
                    result = func.Invoke();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    m_Session.Dispose();
                    m_Session = m_SessionFactory.OpenSession();
                    throw ex;
                }
            }
            return result;
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
