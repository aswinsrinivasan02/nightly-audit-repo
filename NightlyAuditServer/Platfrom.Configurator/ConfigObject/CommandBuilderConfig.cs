using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BallyTech.Infrastructure.Data.MSSql;

namespace PlatformConfigurator
{
    [Serializable]
    public class CommandBuilderConfig : ISerializable
    {
        public List<CommandBuilder> CommandBuilderCollection
        {
            get;
            set;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}