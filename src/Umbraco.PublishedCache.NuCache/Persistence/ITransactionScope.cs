using System;
using System.Collections.Generic;
using System.Text;

namespace Umbraco.Cms.Infrastructure.PublishedCache.Persistence
{
    /// <summary>
    /// Transaction Scope for NuCache Operations
    /// </summary>
    public interface ITransactionScope : IDisposable
    {
        /// <summary>
        /// Commit transaction
        /// </summary>
        void Commit();
        /// <summary>
        /// Rollback transaction
        /// </summary>
        void Rollback();
    }
}
