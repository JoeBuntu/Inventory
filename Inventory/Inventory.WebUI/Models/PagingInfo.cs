using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.WebUI.Models
{
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int IsFirst { get { return CurrentPage = 1; } }
        public int IsLast { get { return CurrentPage = TotalPages; } }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
        public string SortColumn { get; set; }
        public bool SortAsc { get; set; }
    }
}