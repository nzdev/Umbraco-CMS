namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class MacroDtoEntityTypeConfiguration : IEntityTypeConfiguration<MacroDto>
    {
        public void Configure(EntityTypeBuilder<MacroDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Macro);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UniqueId).HasColumnName("uniqueId");
            builder.HasIndex(x => x.UniqueId).IsUnique(true);
            builder.Property(x => x.UseInEditor).HasColumnName("macroUseInEditor");
            builder.Property(x => x.UseInEditor).HasDefaultValue(0);
            builder.Property(x => x.RefreshRate).HasColumnName("macroRefreshRate");
            builder.Property(x => x.RefreshRate).HasDefaultValue(0);
            builder.Property(x => x.Alias).HasColumnName("macroAlias");
            builder.HasIndex(x => x.Alias).IsUnique(true);
            builder.Property(x => x.Name).HasColumnName("macroName");
            builder.Property(x => x.Name).IsRequired(false);
            builder.Property(x => x.CacheByPage).HasColumnName("macroCacheByPage");
            builder.Property(x => x.CacheByPage).HasDefaultValue(1);
            builder.Property(x => x.CachePersonalized).HasColumnName("macroCachePersonalized");
            builder.Property(x => x.CachePersonalized).HasDefaultValue(0);
            builder.Property(x => x.DontRender).HasColumnName("macroDontRender");
            builder.Property(x => x.DontRender).HasDefaultValue(0);
            builder.Property(x => x.MacroSource).HasColumnName("macroSource");
            builder.Property(x => x.MacroSource).IsRequired(true);
            builder.Property(x => x.MacroType).HasColumnName("macroType");
            builder.Property(x => x.MacroType).IsRequired(true);
            builder.HasMany(typeof(MacroPropertyDto), "Macro");
        }
    }
}