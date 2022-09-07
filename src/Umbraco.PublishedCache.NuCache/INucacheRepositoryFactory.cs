using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Cms.Infrastructure.PublishedCache
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
        INucacheContentRepository GetContentRepository();

        bool ContentRespositoryStorageExists();

        /// <summary>
        /// Get an instance of the NuCache media repository
        /// </summary>
        /// <returns></returns>
        INucacheMediaRepository GetMediaRepository();


        bool MediaRespositoryStorageExists();

    }
}
