namespace Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class Member2MemberGroupDtoEntityTypeConfiguration : IEntityTypeConfiguration<Member2MemberGroupDto>
    {
        public void Configure(EntityTypeBuilder<Member2MemberGroupDto> builder)
        {
            builder.ToTable(Cms.Core.Constants.DatabaseSchema.Tables.Member2MemberGroup);
            builder.HasKey(x => new
            {
            x.Member, x.MemberGroup
            }).HasName("PK_cmsMember2MemberGroup");
            builder.Property(x => x.Member).ValueGeneratedNever();
            builder.Property(x => x.Member).HasColumnName("Member");
            builder.HasOne(typeof(MemberDto)).WithOne();
            builder.Property(x => x.MemberGroup).HasColumnName("MemberGroup");
            builder.HasOne(typeof(NodeDto)).WithOne();
        }
    }
}