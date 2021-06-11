namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class User2NodeNotifyDtoEntityTypeConfiguration : IEntityTypeConfiguration<User2NodeNotifyDto>
    {
        public void Configure(EntityTypeBuilder<User2NodeNotifyDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.User2NodeNotify);
            builder.HasKey(x => new
            {
            x.UserId, x.NodeId, x.Action
            }).HasName("PK_umbracoUser2NodeNotify");
            builder.Property(x => x.UserId).ValueGeneratedNever();
            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasOne(typeof(UserDto)).WithOne();
            builder.Property(x => x.NodeId).HasColumnName("nodeId");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.Property(x => x.Action).HasColumnName("action");
            builder.Property(x => x.Action).HasMaxLength(1);
            builder.Property(x => x.Action).HasColumnType("nchar");
        }
    }
}