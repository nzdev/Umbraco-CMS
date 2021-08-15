namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class AccessRuleDtoEntityTypeConfiguration : IEntityTypeConfiguration<AccessRuleDto>
    {
        public void Configure(EntityTypeBuilder<AccessRuleDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.AccessRule);
            builder.HasKey(x => x.Id).HasName("PK_umbracoAccessRule");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.AccessId).HasColumnName("accessId");
            builder.HasOne(typeof(AccessDto), "FK_umbracoAccessRule_umbracoAccess_id").WithOne();
            builder.Property(x => x.RuleValue).HasColumnName("ruleValue");
            builder.HasIndex(x => x.RuleValue).IsUnique(true);
            builder.Property(x => x.RuleType).HasColumnName("ruleType");
            builder.Property(x => x.CreateDate).HasColumnName("createDate");
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.UpdateDate).HasColumnName("updateDate");
            builder.Property(x => x.UpdateDate).HasDefaultValueSql("getdate()");
        }
    }
}