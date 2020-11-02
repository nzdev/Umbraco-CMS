using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public class UmbracoRoutingConventionPropertySelector : IRoutingProperties
    {
        IDictionary<string, string> _umbracoConventions;
        public UmbracoRoutingConventionPropertySelector()
        {
            _umbracoConventions = new Dictionary<string, string>
            {
                { Constants.Conventions.Content.InternalRedirectId,Constants.Conventions.Content.InternalRedirectId },
                { Constants.Conventions.Content.NaviHide,Constants.Conventions.Content.NaviHide },
                { Constants.Conventions.Content.Redirect,Constants.Conventions.Content.Redirect },
                { Constants.Conventions.Content.UrlName,Constants.Conventions.Content.UrlName },
                { Constants.Conventions.Content.UrlAlias,Constants.Conventions.Content.UrlAlias }
            };
        }

        public IReadOnlyDictionary<string, string> EagerLoadProperties => (IReadOnlyDictionary<string, string>)_umbracoConventions;
    }
}
