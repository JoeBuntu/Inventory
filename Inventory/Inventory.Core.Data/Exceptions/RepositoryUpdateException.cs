using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Core.Data.Exceptions
{
    public class RepositoryUpdateException : RepositoryExceptionBase
    {
        public Error ErrorType { get; set; }

        public RepositoryUpdateException(string message, Exception exception)
            : base(message, exception)
        {
            ErrorType = Error.NotSpecified;
        }

        public enum Error
        {
            Version,
            Deleted,
            NotSpecified
        }
    }
}
