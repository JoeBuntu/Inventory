using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public class BOMsRepository : Repository<BOM>, IBOMsRepository
    {
        public BOMsRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }
    }
}
