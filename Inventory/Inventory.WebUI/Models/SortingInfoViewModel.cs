using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Inventory.WebUI.Models
{
    public class SortingInfoViewModel : ViewModelBase
    {
        public string Column { get; set; }
        public bool IsAscending { get; set; }
        
        public RouteValueDictionary GetRouteValues()
        {
            return new RouteValueDictionary(new { sort_col = Column, sort_asc = IsAscending });
        }

        public RouteValueDictionary GetRouteValues(string column_name)
        {
            RouteValueDictionary result = new RouteValueDictionary();
            result.Add("sort_col", column_name);
            if (Column == column_name)
            {
                result.Add("sort_asc", !IsAscending);
            }
            else
            {
                result.Add("sort_asc", true);
            }
            return result;
        }
    }    
}