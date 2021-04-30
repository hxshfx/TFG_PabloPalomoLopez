using System;
using System.Runtime.Serialization;

namespace OCDS_Mapper.src.Exceptions
{
    [Serializable]
    public class InvalidOperationCodeException : Exception
    {
        public InvalidOperationCodeException() {}
        protected InvalidOperationCodeException(SerializationInfo info, StreamingContext ctx)
            : base(info, ctx) {}
    }
}
