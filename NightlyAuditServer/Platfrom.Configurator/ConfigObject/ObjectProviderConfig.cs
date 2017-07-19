using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BallyTech.Infrastructure.Data.MSSql;

namespace PlatformConfigurator
{
    [Serializable]
    public class ObjectProviderConfig : ISerializable
    {
        public List<ObjectProvider> ObjectProviderCollection
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