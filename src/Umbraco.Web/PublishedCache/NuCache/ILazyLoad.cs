using System;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface ILazyLoad<TKey,TValue>
    {
        void SetLazyLoader(Func<TKey, TValue> lazyLoader);
    }
}
