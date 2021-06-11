namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class MediaDtoEntityTypeConfiguration : IEntityTypeConfiguration<MediaDto>
    {
        public void Configure(EntityTypeBuilder<MediaDto> builder)
        {
        }
    }
}