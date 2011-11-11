using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Inventory.Core.Entities;

namespace Inventory.Core.Data
{
    public class MaterialsRepository : Repository<Material>, IMaterialsRepository
    {
        public MaterialsRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }
    }
}
