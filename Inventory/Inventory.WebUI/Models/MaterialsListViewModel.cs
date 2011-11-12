using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Inventory.Core.Entities;

namespace Inventory.WebUI.Models
{
    public class MaterialsListViewModel : ViewModelBase
    {
        public IEnumerable<Material> Materials { get; set; }
        public PagingInfoViewModel PagingInfo { get; set; }
        public SortingInfoViewModel SortingInfo { get; set; }
        public RouteValueDictionary GetRouteValues(string column_name)
        {
            return RouteValueCombiner.Combine(PagingInfo.GetRouteValues(), SortingInfo.GetRouteValues(column_name));
        }
    }
}