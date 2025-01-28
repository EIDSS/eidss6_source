using System;

namespace EIDSS.EHS.Service
{
    public class EhsDataException : ApplicationException
    {
        public EhsDataException()
        {
        }

        public EhsDataException(string message) : base(message)
        {
        }

        public EhsDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}