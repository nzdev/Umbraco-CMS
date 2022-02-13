using System;
using System.Collections.Generic;
using System.Text;

namespace Umbraco.Cms.Infrastructure.PublishedCache.Persistence
{
    /// <summary>
    /// Builds NuCache Repositories
    /// </summary>
    public interface INucacheRepositoryFactory
    {
        /// <summary>
        /// Get an instance of the NuCache document repository
        /// </summary>
        /// <returns></returns>
        INucacheNoSqlContentRepository GetContentRepository();
        /// <summary>
        /// Get an instance of the NuCache media repository
        /// </summary>
        /// <returns></returns>
        INucacheNoSqlMediaRepository GetMediaRepository();
    }
}
