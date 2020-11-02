using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface ILazyLoadingTransactableDictionary<TKey,TValue> : ITransactableDictionary<TKey,TValue>
    {
        #region IEnumerable
        IEnumerable<KeyValuePair<TKey,TValue>> GetEnumerator(ContentNodeKitLoadState requestedLoadState);
        #endregion
    }
}
