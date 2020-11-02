﻿using System.Collections.Generic;
using System.IO;
using CSharpTest.Net.Serialization;

namespace Umbraco.Web.PublishedCache.NuCache.DataSource
{
    /// <summary>
    /// Serializes/Deserializes data to BTree data source for <see cref="ContentData"/>
    /// </summary>
    internal class ContentDataSerializer : ISerializer<IContentData>
    {
        public ContentDataSerializer(IDictionaryOfPropertyDataSerializer dictionaryOfPropertyDataSerializer, ISerializer<IReadOnlyDictionary<string, CultureVariation>> cultureVariationsSerializer)
        {
            _dictionaryOfPropertyDataSerializer = dictionaryOfPropertyDataSerializer;
            _cultureVariationsSerializer = cultureVariationsSerializer;
        }
        private readonly IDictionaryOfPropertyDataSerializer _dictionaryOfPropertyDataSerializer;
        private readonly ISerializer<IReadOnlyDictionary<string, CultureVariation>> _cultureVariationsSerializer;

        public IContentData ReadFrom(Stream stream)
        {
            return new ContentData
            {
                Published = PrimitiveSerializer.Boolean.ReadFrom(stream),
                Name = PrimitiveSerializer.String.ReadFrom(stream),
                UrlSegment = PrimitiveSerializer.String.ReadFrom(stream),
                VersionId = PrimitiveSerializer.Int32.ReadFrom(stream),
                VersionDate = PrimitiveSerializer.DateTime.ReadFrom(stream),
                WriterId = PrimitiveSerializer.Int32.ReadFrom(stream),
                TemplateId = PrimitiveSerializer.Int32.ReadFrom(stream),
                Properties = _dictionaryOfPropertyDataSerializer.ReadFrom(stream), // TODO: We don't want to allocate empty arrays
                CultureInfos = _cultureVariationsSerializer.ReadFrom(stream) // TODO: We don't want to allocate empty arrays
            };
        }

        public void WriteTo(IContentData value, Stream stream)
        {
            PrimitiveSerializer.Boolean.WriteTo(value.Published, stream);
            PrimitiveSerializer.String.WriteTo(value.Name, stream);
            PrimitiveSerializer.String.WriteTo(value.UrlSegment, stream);
            PrimitiveSerializer.Int32.WriteTo(value.VersionId, stream);
            PrimitiveSerializer.DateTime.WriteTo(value.VersionDate, stream);
            PrimitiveSerializer.Int32.WriteTo(value.WriterId, stream);
            if (value.TemplateId.HasValue)
            {
                PrimitiveSerializer.Int32.WriteTo(value.TemplateId.Value, stream);
            }
            _dictionaryOfPropertyDataSerializer.WriteTo(value.Properties, stream);
            _cultureVariationsSerializer.WriteTo(value.CultureInfos, stream);
        }
    }
}
