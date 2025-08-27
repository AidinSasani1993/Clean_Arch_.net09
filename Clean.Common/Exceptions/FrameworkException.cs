using System.Runtime.Serialization;

namespace Clean.Common.Exceptions
{
    public class FrameworkException : Exception
    {
        public FrameworkException()
        {
        }

        public FrameworkException(string? message) : base(message)
        {
        }

        public FrameworkException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FrameworkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
