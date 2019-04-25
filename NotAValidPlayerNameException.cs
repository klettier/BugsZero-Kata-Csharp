using System;
using System.Runtime.Serialization;

namespace Trivia
{
    class NotAValidPlayerNameException : Exception
    {
        public NotAValidPlayerNameException()
        {
        }

        public NotAValidPlayerNameException(string message) : base(message)
        {
        }

        public NotAValidPlayerNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotAValidPlayerNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
