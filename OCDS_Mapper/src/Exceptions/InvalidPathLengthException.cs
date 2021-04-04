using System;
using System.Runtime.Serialization;

namespace OCDS_Mapper.src.Exceptions
{
    [Serializable]
    public class InvalidPathLengthException : Exception
    {
        public InvalidPathLengthException() {}
        protected InvalidPathLengthException(SerializationInfo info, StreamingContext ctx)
            : base(info, ctx) {}
    }
}