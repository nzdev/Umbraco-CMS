using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Core;
using Umbraco.Web.PublishedCache.NuCache;
using LiteDB;
using Umbraco.Web.PublishedCache.NuCache.DataSource;
using Umbraco.PublishedCache.NuCache.LiteDb.NuCache;
using Umbraco.Web.PublishedCache;
using Umbraco.PublishedCache.NuCache.LiteDb.Routing;

namespace Umbraco.PublishedCache.NuCache.LiteDb.Composing
{
    [ComposeAfter(typeof(NuCacheComposer))]
    public class LiteDbNuCacheComposer : ComponentComposer<LiteDbNuCacheComponent>, ICoreComposer
    {
        public override void Compose(Composition composition)
        {
            base.Compose(composition);

            composition.Register<LiteDbTransactableDictionaryFactory, LiteDbTransactableDictionaryFactory>(Lifetime.Singleton);
            composition.Register<ITransactableDictionaryFactory, LiteDbTransactableDictionaryFactory>(Lifetime.Singleton);

            BsonMapper.Global.RegisterType<ContentNodeKit>
              (
                  serialize: (cn) => NuCacheMapper.SerializeContentNodeKit(cn),
                  deserialize: (cn) =>
                  {
                      return NuCacheMapper.DeserializeToContentNodeKit(cn);
                  }
              );

            
            composition.Register<LiteDbContentSettings>((f) =>
            {
                var liteDbNuCacheFactory = f.GetInstance<LiteDbTransactableDictionaryFactory>();
                var liteDbSettings = new LiteDbContentSettings()
                {
                    ConnectionString = new ConnectionString()
                    {
                        Filename = liteDbNuCacheFactory.GetContentDbPath(),
                        ReadOnly = true
                    },
                    CollectionName = liteDbNuCacheFactory.ContentCollectionName()
                };
                return liteDbSettings;
            }, Lifetime.Singleton);
            composition.Register<IContentRouter, LiteDbContentCacheContentRouter>(Lifetime.Singleton);
        }

    }
}
