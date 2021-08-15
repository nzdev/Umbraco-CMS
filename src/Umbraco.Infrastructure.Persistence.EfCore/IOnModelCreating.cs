using Microsoft.EntityFrameworkCore;

namespace Umbraco.Cms.Infrastructure.Persistence.EfCore
{
    internal interface IOnModelCreating
    {
        void OnModelCreating(ModelBuilder builder);
    }
}
