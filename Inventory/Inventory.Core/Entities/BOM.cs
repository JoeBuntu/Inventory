using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Core.Entities
{
    public class BOM : EntityBase
    {
        public virtual Material Product { get; set; }
        public virtual IList<Material> Components { get; set; }
    }
}
