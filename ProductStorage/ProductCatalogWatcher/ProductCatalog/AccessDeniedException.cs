using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalogWatcher.ProductCatalog
{
    public class AccessDeniedException : ApplicationException
    {
        public AccessDeniedException()
        {
        }

        public AccessDeniedException(string message) : base(message)
        {

        }

        public AccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
