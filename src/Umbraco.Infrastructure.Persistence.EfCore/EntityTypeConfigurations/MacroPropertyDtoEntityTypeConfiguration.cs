namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class MacroPropertyDtoEntityTypeConfiguration : IEntityTypeConfiguration<MacroPropertyDto>
    {
        public void Configure(EntityTypeBuilder<MacroPropertyDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.MacroProperty);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UniqueId).HasColumnName("uniquePropertyId");
            builder.HasIndex(x => x.UniqueId).IsUnique(true);
            builder.Property(x => x.EditorAlias).HasColumnName("editorAlias");
            builder.Property(x => x.Macro).HasColumnName("macro");
            builder.HasOne(typeof(MacroDto)).WithOne();
            builder.HasIndex(x => x.Macro).IsUnique(true);
            builder.Property(x => x.SortOrder).HasColumnName("macroPropertySortOrder");
            builder.Property(x => x.SortOrder).HasDefaultValue(0);
            builder.Property(x => x.Alias).HasColumnName("macroPropertyAlias");
            builder.Property(x => x.Alias).HasMaxLength(50);
            builder.Property(x => x.Name).HasColumnName("macroPropertyName");
        }
    }
}