using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    /// <summary>
    /// Provides serialization for a type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITransactableDictionarySerializer<T>
    {
        /// <summary>
        /// Reads the object from a stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        T ReadFrom(Stream stream);
        /// <summary>
        /// Writes the object to the stream
        /// </summary>
        /// <param name="value"></param>
        /// <param name="stream"></param>
        void WriteTo(T value, Stream stream);
    }
}
