﻿using System.Configuration;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web.PublishedCache.NuCache.DataSource;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public class NuCacheComposer : ComponentComposer<NuCacheComponent>, ICoreComposer
    {
        public override void Compose(Composition composition)
        {
            base.Compose(composition);

            var serializer = ConfigurationManager.AppSettings["Umbraco.Web.PublishedCache.NuCache.Serializer"];

            if (serializer == "MsgPack")
            {
                composition.Register<IContentNestedDataSerializer, MsgPackContentNestedDataSerializer>();                
            }
            else
            {
                composition.Register<IContentNestedDataSerializer, JsonContentNestedDataSerializer>();
            }

            // register the NuCache database data source
            composition.Register<IDataSource, DatabaseDataSource>();

            // register the NuCache published snapshot service
            // must register default options, required in the service ctor
            composition.Register(factory => new PublishedSnapshotServiceOptions());
            composition.SetPublishedSnapshotService<PublishedSnapshotService>();

          
            composition.Register<IPublishedCachePropertyKeyMapper, DefaultPublishedCachePropertyKeyMapper>(Lifetime.Singleton);

            // add the NuCache health check (hidden from type finder)
            // TODO: no NuCache health check yet
            //composition.HealthChecks().Add<NuCacheIntegrityHealthCheck>();
        }
    }
}
