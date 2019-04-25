using System;
using System.Runtime.Serialization;

namespace Trivia
{
    class NotEnoughPlayerException : Exception
    {
        public NotEnoughPlayerException()
        {
        }

        public NotEnoughPlayerException(string message) : base(message)
        {
        }

        public NotEnoughPlayerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughPlayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
