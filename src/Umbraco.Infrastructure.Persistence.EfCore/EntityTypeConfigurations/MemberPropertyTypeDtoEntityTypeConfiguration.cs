namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class MemberPropertyTypeDtoEntityTypeConfiguration : IEntityTypeConfiguration<MemberPropertyTypeDto>
    {
        public void Configure(EntityTypeBuilder<MemberPropertyTypeDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.MemberPropertyType);
            builder.HasKey(x => x.PrimaryKey);
            builder.Property(x => x.PrimaryKey).HasColumnName("pk");
            builder.Property(x => x.NodeId).HasColumnName("NodeId");
            builder.HasOne(typeof(ContentTypeDto)).WithOne();
            builder.Property(x => x.PropertyTypeId).HasColumnName("propertytypeId");
            builder.Property(x => x.CanEdit).HasColumnName("memberCanEdit");
            builder.Property(x => x.CanEdit).HasDefaultValue(0);
            builder.Property(x => x.ViewOnProfile).HasColumnName("viewOnProfile");
            builder.Property(x => x.ViewOnProfile).HasDefaultValue(0);
            builder.Property(x => x.IsSensitive).HasColumnName("isSensitive");
            builder.Property(x => x.IsSensitive).HasDefaultValue(0);
        }
    }
}