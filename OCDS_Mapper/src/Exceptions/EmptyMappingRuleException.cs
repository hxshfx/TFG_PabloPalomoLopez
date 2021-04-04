using System;
using System.Runtime.Serialization;

namespace OCDS_Mapper.src.Exceptions
{
    [Serializable]
    public class EmptyMappingRuleException : Exception
    {
        public EmptyMappingRuleException() {}
        protected EmptyMappingRuleException(SerializationInfo info, StreamingContext ctx)
            : base(info, ctx) {}
    }
}