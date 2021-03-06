﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Core.Data.Exceptions
{
    public class RepositoryExceptionBase : Exception
    {        
        public object OldItem { get; set; }
        public object NewItem { get; set; }

        public RepositoryExceptionBase(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
