using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Inventory.WebUI.Models
{
    public class ViewModelBase
    {
        public RouteValueDictionary RouteValues { get; set; }
    }
}