using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Core.Entities;

namespace Inventory.WebUI.Models
{
    public class MaterialsListViewModel
    {
        public IEnumerable<Material> Materials { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}