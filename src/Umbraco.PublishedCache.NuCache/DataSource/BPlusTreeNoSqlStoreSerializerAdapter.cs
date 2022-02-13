using System;
using System.Collections.Generic;
using System.Text;
using CSharpTest.Net.Serialization;
using System.IO;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    /// <summary>
    /// Adapts ISerializer<T> to INoSqlStoreSerializer<T>
    /// </summary>
    /// <typeparam name="T">Type to serialize/deserialize</typeparam>
    public class BPlusTreeNoSqlStoreSerializerAdapter<T> : INoSqlStoreSerializer<T>
    {
        private readonly ISerializer<T> _serializer;

        public BPlusTreeNoSqlStoreSerializerAdapter(ISerializer<T> serializer)
        {
            _serializer = serializer;
        }
        public T ReadFrom(Stream stream)
        {
            return _serializer.ReadFrom(stream);
        }

        public void WriteTo(T value, Stream stream)
        {
            _serializer.WriteTo(value, stream);
        }
    }
}
