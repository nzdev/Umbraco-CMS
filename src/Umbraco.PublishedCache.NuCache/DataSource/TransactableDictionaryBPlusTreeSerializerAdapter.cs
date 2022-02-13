using System;
using System.Collections.Generic;
using System.Text;
using CSharpTest.Net.Serialization;
using System.IO;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    /// <summary>
    /// Adapts ITransactableDictionarySerializer<T> to ISerializer<T>
    /// </summary>
    /// <typeparam name="T">Type to serialize/deserialize</typeparam>
    public class TransactableDictionaryBPlusTreeSerializerAdapter<T> : ISerializer<T>
    {
        private readonly ITransactableDictionarySerializer<T> _serializer;

        public TransactableDictionaryBPlusTreeSerializerAdapter(ITransactableDictionarySerializer<T> serializer)
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
