using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.WebUI.Models
{
    public class PagingInfo
    {
        private int m_GroupSize = 5;

        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        public int CurrentPage { get; set; }
        public bool IsFirst { get { return CurrentPage == 1; } }
        public bool IsLast { get { return CurrentPage == TotalPages; } }

        public int GroupSize
        {
            get { return m_GroupSize; }
            set
            {
                if (m_GroupSize % 2 > 0)
                    throw new ArgumentException("Group Size not set to an odd number");
                m_GroupSize = value;
            }
        }
        public bool HasLeftGroupGap
        {
            get { return GroupStart > 1; }
        }

        public bool HasRightGroupGap
        {
            get { return GroupEnd < TotalPages; }
        }

        public int GroupStart
        {
            get
            {
                if (TotalPages > GroupSize)
                {
                    int sideCount = (int) ( GroupSize / 2);
                    if (CurrentPage - sideCount > 2)
                    {
                        if (CurrentPage + sideCount < TotalPages - 1)
                        {
                            return CurrentPage - sideCount;
                        }
                        else
                        {
                            return TotalPages - GroupSize + 1;
                        }
                    }
                }
                return 1;
            }
        }

        public int GroupEnd
        {
            get
            {
                if (TotalPages > GroupSize)
                {
                    int sideCount = (int)(GroupSize / 2);
                    if (CurrentPage + sideCount < TotalPages - 1)
                    {
                        if (CurrentPage - sideCount  > 2)
                        {
                            return CurrentPage + sideCount;
                        }
                        else
                        {
                            return GroupSize;
                        }
                    }
                }
                return TotalPages;
            }
        }

        public string SortColumn { get; set; }
        public bool SortAsc { get; set; }
    }
}