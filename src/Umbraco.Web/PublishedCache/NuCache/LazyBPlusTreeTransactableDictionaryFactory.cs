using CSharpTest.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Configuration;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public class LazyBPlusTreeTransactableDictionaryFactory : BPlusTreeTransactableDictionaryFactory
    {
        private readonly IBplusTreeLazySerializers _bplusTreeLazySerializers;
        public LazyBPlusTreeTransactableDictionaryFactory(IGlobalSettings globalSettings,
            ISerializer<IContentNodeKit> contentDataSerializer, IBplusTreeLazySerializers bplusTreeLazySerializers) : base(globalSettings, contentDataSerializer)
        {
            _bplusTreeLazySerializers = bplusTreeLazySerializers;
        }
        protected override ITransactableDictionary<int, IContentNodeKit> GetDocumentBPlusTree(string localContentDbPath, bool localContentCacheFilesExist)
        {
            var fullLoadLazyDictionary = GetTree(localContentDbPath, localContentCacheFilesExist, _bplusTreeLazySerializers?.ContentDataSerializer);
            var routingOnlyDocumentDictionary = GetTree(localContentDbPath, localContentCacheFilesExist, _bplusTreeLazySerializers?.RoutingPropertiesOnlySerializer, true);
            var routingAndDraftOnlyDocumentDictionary = GetTree(localContentDbPath, localContentCacheFilesExist, _bplusTreeLazySerializers?.RoutingAndDraftPropertiesOnlySerializer, true);
            var routingAndPublishedOnlyDocumentDictionary = GetTree(localContentDbPath, localContentCacheFilesExist, _bplusTreeLazySerializers?.RoutingAndDraftPropertiesOnlySerializer, true);
            return new LazyLoadingBPlusTreeTransactableDictionary<int, IContentNodeKit>(fullLoadLazyDictionary, localContentDbPath, localContentCacheFilesExist, routingOnlyDocumentDictionary, routingAndDraftOnlyDocumentDictionary, routingAndPublishedOnlyDocumentDictionary);
        }
    }
}
