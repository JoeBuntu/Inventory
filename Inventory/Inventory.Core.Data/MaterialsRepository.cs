﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public class MaterialsRepository
    {
        ISessionFactory m_SessionFactory;
        ISession m_Session;

        public MaterialsRepository(ISessionFactory sessionFactory)
        {
            m_SessionFactory = sessionFactory;
            m_Session = m_SessionFactory.OpenSession();
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
                    result = m_Session.Query<Material>().Skip(0).Take(count).ToList();
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