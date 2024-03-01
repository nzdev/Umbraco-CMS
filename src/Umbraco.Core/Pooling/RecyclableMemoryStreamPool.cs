using Microsoft.IO;

namespace Umbraco.Cms.Core.Pooling
{
    /// <summary>
    /// Provides an in memory Stream instances from a pool.
    /// </summary>
    public class RecyclableMemoryStreamPool : IMemoryStreamPool
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RecyclableMemoryStreamPool()
        {
            RecyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        /// <summary>
        /// Gets the RecyclableMemoryStreamManager.
        /// </summary>
        internal RecyclableMemoryStreamManager RecyclableMemoryStreamManager { get; }

        /// <summary>
        /// Retrieve a new <see cref="PooledMemoryStream"/> object with no tag and a default initial capacity.
        /// </summary>
        /// <returns></returns>
        public PooledMemoryStream GetStream() => new PooledMemoryStream(RecyclableMemoryStreamManager.GetStream());
    }
}
