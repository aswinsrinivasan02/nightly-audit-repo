using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BallyTech.Infrastructure.Data;

namespace PlatformConfigurator
{
    [Serializable]
    public class DataConfigurationSection : ISerializable
    {
        public List<DataProviderAdapterElement> DataServiceElementCollection
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