using System;
using System.Runtime.Serialization;

namespace OCDS_Mapper.src.Exceptions
{
    [Serializable]
    public class WrongMappingException : Exception
    {
        public WrongMappingException() {}

        public WrongMappingException(string MappingElement) 
            : base($"The mapping element {MappingElement} isn't correct") {}

        protected WrongMappingException(SerializationInfo info, StreamingContext ctx)
            : base(info, ctx) {}
    }
}
