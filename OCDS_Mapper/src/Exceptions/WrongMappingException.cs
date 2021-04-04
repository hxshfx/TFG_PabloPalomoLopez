using System;
using System.Runtime.Serialization;

namespace OCDS_Mapper.src.Exceptions
{
    [Serializable]
    public class WrongMappingException : Exception
    {
        public WrongMappingException() {}

        public WrongMappingException(string mappingElement) 
            : base($"The mapping element {mappingElement} isn't correct") {}

        public WrongMappingException(string mappingElement, string mappingValue)
            : base($"The value \'{mappingValue}\' in mapping element \'{mappingElement}\' isn't correct") {}

        protected WrongMappingException(SerializationInfo info, StreamingContext ctx)
            : base(info, ctx) {}
    }
}