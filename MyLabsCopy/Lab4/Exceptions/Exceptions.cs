using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab4.Exceptions
{
    class DAOException : Exception
    {
        public DAOException()
            : base()
        {
        }

        public DAOException(string message)
            : base(message)
        {
        }

    }

    class ShopServiceException : Exception
    {
        public ShopServiceException()
            : base()
        { }
        public ShopServiceException(string message)
            : base(message)
        { }
    }
}
