using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Core.Entities
{
    public class BOMComponent : EntityBase
    {
        public virtual Material Component { get; set; }
        public virtual UnitType Unit { get; set; }
        public virtual decimal Quantity { get; set; }
    }
}
