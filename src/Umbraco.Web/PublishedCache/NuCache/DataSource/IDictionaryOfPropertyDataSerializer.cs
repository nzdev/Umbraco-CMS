using System.Collections.Generic;
using System.IO;

namespace Umbraco.Web.PublishedCache.NuCache.DataSource
{
    internal interface IDictionaryOfPropertyDataSerializer
    {
        IDictionary<string, IPropertyData[]> ReadFrom(Stream stream);
        void WriteTo(IDictionary<string, IPropertyData[]> value, Stream stream);
    }
}
