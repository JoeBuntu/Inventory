using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public class MaterialsRepository : IMaterialsRepository
    {
        ISessionFactory m_SessionFactory;
        ISession m_Session;

        public MaterialsRepository(ISessionFactory sessionFactory)
        {
            m_SessionFactory = sessionFactory;
            m_Session = m_SessionFactory.OpenSession();
        }

        public int Count
        {
            get
            {
                int result;
                using (ITransaction trxn = m_Session.BeginTransaction())
                {
                    result = m_Session.Query<Material>().Count();
                    trxn.Commit();
                }
                return result;
            }
        }

        public Material Get(int id)
        {
            Material result = null;
            using (ITransaction trxn = m_Session.BeginTransaction())
            {
                try
                {
                    result = m_Session.Get<Material>(id);
                    trxn.Commit();
                }
                catch (Exception ex)
                {
                    trxn.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        public List<Material> Get(int start_index, int count)
        {
            List<Material> result = null;
            using (ITransaction trxn = m_Session.BeginTransaction())
            {
                try
                {
                    result = m_Session.Query<Material>().Skip(start_index).Take(count).ToList();
                    trxn.Commit();
                }
                catch (Exception ex)
                {
                    trxn.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        public IList<Material> Get(int start_index, int count, string sort_column, bool sort_asc)
        {
            IList<Material> result = null;
            using (ITransaction trxn = m_Session.BeginTransaction())
            {
                try
                {
                    result = m_Session.CreateCriteria<Material>()
                              .AddOrder(new NHibernate.Criterion.Order(sort_column, sort_asc))
                              .SetFirstResult(start_index)
                              .SetMaxResults(count)
                              .List<Material>();                             
                    trxn.Commit();
                }
                catch (Exception ex)
                {
                    trxn.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        public int Add(Material material)
        {
            int result;
            using (ITransaction trxn = m_Session.BeginTransaction())
            {
                try
                {
                    result = (int)m_Session.Save(material);
                    trxn.Commit();
                }
                catch (Exception ex)
                {
                    trxn.Rollback();
                    m_Session.Dispose();
                    m_Session = m_SessionFactory.OpenSession();
                    throw ex;
                }
            }
            return result;
        }

        public void Update(Material material)
        {
            using (ITransaction trxn = m_Session.BeginTransaction())
            {
                try
                {
                    m_Session.Update(material);
                    trxn.Commit();
                }
                catch (Exception ex)
                {
                    trxn.Rollback();
                    m_Session.Dispose();
                    m_Session = m_SessionFactory.OpenSession();
                    throw ex;
                }
            }
        }

        public void Delete(int id)
        {
            Material material = Get(id);
            this.Delete(material);
        }

        public void Delete(Material material)
        {
            using (ITransaction trxn = m_Session.BeginTransaction())
            {
                try
                {
                    m_Session.Delete(material);
                    trxn.Commit();
                }
                catch (Exception ex)
                {
                    trxn.Rollback();
                    throw ex;
                }
            }
        }
    }
}
