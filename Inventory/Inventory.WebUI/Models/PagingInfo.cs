using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.WebUI.Models
{
    public class PagingInfo
    {
        private int m_BlockSize = 5;

        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        public int CurrentPage { get; set; }
        public bool IsFirst { get { return CurrentPage == 1; } }
        public bool IsLast { get { return CurrentPage == TotalPages; } }

        public int BlockSize
        {
            get { return m_BlockSize; }
            set
            {
                if (m_BlockSize % 2 > 0)
                    throw new ArgumentException("Group Size not set to an odd number");
                m_BlockSize = value;
            }
        }
        public bool HasLeftGroupGap
        {
            get { return BlockStart > 1; }
        }

        public bool HasRightGroupGap
        {
            get { return BlockEnd < TotalPages; }
        }

        public int BlockStart
        {
            get
            {
                if (TotalPages > BlockSize)
                {
                    int sideCount = (int) ( BlockSize / 2);
                    if (CurrentPage - sideCount > 2)
                    {
                        if (CurrentPage + sideCount < TotalPages - 1)
                        {
                            return CurrentPage - sideCount;
                        }
                        else
                        {
                            return TotalPages - BlockSize + 1;
                        }
                    }
                }
                return 1;
            }
        }

        public int BlockEnd
        {
            get
            {
                if (TotalPages > BlockSize)
                {
                    int sideCount = (int)(BlockSize / 2);
                    if (CurrentPage + sideCount < TotalPages - 1)
                    {
                        if (CurrentPage - sideCount  > 2)
                        {
                            return CurrentPage + sideCount;
                        }
                        else
                        {
                            return BlockSize;
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