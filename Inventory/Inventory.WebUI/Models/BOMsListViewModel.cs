using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Core.Entities;

namespace Inventory.WebUI.Models
{
    public class BOMsListViewModel
    {
        public IEnumerable<BOM> BOMs { get; set; }
        public PagingInfoViewModel PagingInfo { get; set; }
        public SortingInfoViewModel SortingInfo { get; set; }
    }
}