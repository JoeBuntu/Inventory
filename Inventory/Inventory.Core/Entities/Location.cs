using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Core.Entities
{
    public class Location : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool IsSystemLocation { get; set; }
    }
}
