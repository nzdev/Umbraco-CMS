namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class PropertyTypeCommonDtoEntityTypeConfiguration : IEntityTypeConfiguration<PropertyTypeCommonDto>
    {
        public void Configure(EntityTypeBuilder<PropertyTypeCommonDto> builder)
        {
        }
    }
}