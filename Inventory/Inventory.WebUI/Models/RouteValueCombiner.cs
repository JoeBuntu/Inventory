using System; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Reflection;

namespace Inventory.WebUI.Models
{
    public class RouteValueCombiner
    {
        public static RouteValueDictionary Combine(RouteValueDictionary v1, RouteValueDictionary v2)
        {
            return new RouteValueDictionary(v1.Union(v2).ToDictionary(x => x.Key, y => y.Value));
        }

        public static RouteValueDictionary Combine(RouteValueDictionary v1, object obj)
        {
            return Combine(v1, Extract(obj));
        }

        public static RouteValueDictionary Combine(RouteValueDictionary v1, RouteValueDictionary v2, object obj)
        {
            return Combine(v1, Combine(v2, obj));
        }

        private static RouteValueDictionary Extract(Object obj)
        {
            Type objType = obj.GetType();
            return new RouteValueDictionary(
                objType.GetProperties()
                       .ToDictionary(x => x.Name, y => y.GetValue(obj, null))
            );
        }
    }
}