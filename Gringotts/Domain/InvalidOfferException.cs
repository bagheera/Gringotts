using System;
using System.Runtime.Serialization;

namespace Gringotts.Domain
{
    public class InvalidOfferException : Exception
    {
        public InvalidOfferException()
        {
        }

        public InvalidOfferException(string message) : base(message)
        {
        }

        public InvalidOfferException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidOfferException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}