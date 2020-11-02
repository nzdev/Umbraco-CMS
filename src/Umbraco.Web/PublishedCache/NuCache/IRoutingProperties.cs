using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface IRoutingProperties
    {
        IReadOnlyDictionary<string,string> EagerLoadProperties { get; }
    }
}
