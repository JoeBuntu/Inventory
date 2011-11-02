using System;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace Inventory.Core.Entities
{
    public class BOM : EntityBase
    {
        public virtual Material Product { get; set; }
        public virtual ISet<Material> Components { get; set; }
    }
}
