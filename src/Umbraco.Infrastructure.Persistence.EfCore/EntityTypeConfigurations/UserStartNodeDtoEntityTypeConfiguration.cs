namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class UserStartNodeDtoEntityTypeConfiguration : IEntityTypeConfiguration<UserStartNodeDto>
    {
        public void Configure(EntityTypeBuilder<UserStartNodeDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.UserStartNode);
            builder.HasKey(x => x.Id).HasName("PK_userStartNode");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasOne(typeof(UserDto)).WithOne();
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.StartNode).HasColumnName("startNode");
            builder.HasOne(typeof(NodeDto)).WithOne();
            builder.Property(x => x.StartNode).IsRequired(true);
            builder.Property(x => x.StartNodeType).HasColumnName("startNodeType");
            builder.Property(x => x.StartNodeType).IsRequired(true);
            builder.HasIndex(x => x.StartNodeType).IsUnique(true);
        }
    }
}