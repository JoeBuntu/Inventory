using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Core.Entities
{
    public class User : EntityBase
    {
        [Required(ErrorMessage = "User name is required")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "User name must be between 1 and 25 characters")]
        public virtual string Name { get; }
    }
}
