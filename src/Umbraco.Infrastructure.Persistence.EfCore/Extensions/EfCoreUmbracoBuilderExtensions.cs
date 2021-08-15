using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Infrastructure.Persistence.EfCore;

namespace Umbraco.Infrastructure.Persistence.EfCore.Extensions
{
    public static class EfCoreUmbracoBuilderExtensions
    {
        public static IServiceCollection AddEfCoreServices(this IServiceCollection services)
        {
            services.AddDbContext<UmbracoDbContext>(
            options => options.UseSqlServer("name=ConnectionStrings:umbracoDbDSN"));
            return services;
        }
    }
}
