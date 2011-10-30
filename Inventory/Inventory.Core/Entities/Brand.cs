using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Core.Entities
{
    public class Brand : EntityBase
    {
        public virtual string Name { get; set; }
    }
}
