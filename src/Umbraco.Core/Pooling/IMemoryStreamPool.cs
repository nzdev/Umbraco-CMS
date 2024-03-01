namespace Umbraco.Cms.Core.Pooling
{
    /// <summary>
    /// Provides in memory Stream instances from a pool.
    /// </summary>
    public interface IMemoryStreamPool
    {
        /// <summary>
        /// Retrieve a new <see cref="PooledMemoryStream"/> object with an default initial capacity.
        /// </summary>
        /// <returns></returns>
        PooledMemoryStream GetStream();
    }
}
