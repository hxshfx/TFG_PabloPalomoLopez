using System;
using System.Runtime.Serialization;

namespace OCDS_Mapper.src.Exceptions
{
    [Serializable]
    public class InvalidParsedElementException : Exception
    {
        public InvalidParsedElementException() {}

        public InvalidParsedElementException(string id)
            : base($"Element \'ContractFolderStatus\' wasn't found in entry with id: {id}") {}

        protected InvalidParsedElementException(SerializationInfo info, StreamingContext ctx)
            : base(info, ctx) {}
    }
}
