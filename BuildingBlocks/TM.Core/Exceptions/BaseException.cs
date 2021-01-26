using System;
using System.Runtime.Serialization;

namespace TM.Core.Exceptions
{
    /// <summary>
    /// Base exception type for those are thrown by system for specific exceptions.
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="BaseException"/> object.
        /// </summary>
        public BaseException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public BaseException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public BaseException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
