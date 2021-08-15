namespace Umbraco.Cms.Infrastructure.Persistence.EfCore
{
    using Microsoft.EntityFrameworkCore;
    using Umbraco.Cms.Infrastructure.Persistence.Dtos;

    internal class UmbracoDbContext : DbContext
    {
        public DbSet<AccessDto> Access { get; set; }

        public DbSet<AccessRuleDto> AccessRule { get; set; }

        public DbSet<AuditEntryDto> AuditEntry { get; set; }

        public DbSet<CacheInstructionDto> CacheInstruction { get; set; }

        public DbSet<ConsentDto> Consent { get; set; }

        public DbSet<ContentDto> Content { get; set; }

        public DbSet<ContentNuDto> ContentNu { get; set; }

        public DbSet<ContentScheduleDto> ContentSchedule { get; set; }

        public DbSet<ContentType2ContentTypeDto> ContentType2ContentType { get; set; }

        public DbSet<ContentTypeAllowedContentTypeDto> ContentTypeAllowedContentType { get; set; }

        public DbSet<ContentTypeDto> ContentType { get; set; }

        public DbSet<ContentTypeTemplateDto> ContentTypeTemplate { get; set; }

        public DbSet<ContentVersionCultureVariationDto> ContentVersionCultureVariation { get; set; }

        public DbSet<ContentVersionDto> ContentVersion { get; set; }

        public DbSet<DataTypeDto> DataType { get; set; }

        public DbSet<DictionaryDto> Dictionary { get; set; }

        public DbSet<DocumentCultureVariationDto> DocumentCultureVariation { get; set; }

        public DbSet<DocumentDto> Document { get; set; }

        public DbSet<DocumentPublishedReadOnlyDto> DocumentPublishedReadOnly { get; set; }

        public DbSet<DocumentVersionDto> DocumentVersion { get; set; }

        public DbSet<DomainDto> Domain { get; set; }

        public DbSet<ExternalLoginDto> ExternalLogin { get; set; }

        public DbSet<ExternalLoginTokenDto> ExternalLoginToken { get; set; }

        public DbSet<KeyValueDto> KeyValue { get; set; }

        public DbSet<LanguageDto> Language { get; set; }

        public DbSet<LanguageTextDto> LanguageText { get; set; }

        public DbSet<LockDto> Lock { get; set; }

        public DbSet<LogDto> Log { get; set; }

        public DbSet<LogViewerQueryDto> LogViewerQuery { get; set; }

        public DbSet<MacroDto> Macro { get; set; }

        public DbSet<MacroPropertyDto> MacroProperty { get; set; }

        public DbSet<MediaDto> Media { get; set; }

        public DbSet<MediaVersionDto> MediaVersion { get; set; }

        public DbSet<Member2MemberGroupDto> Member2MemberGroup { get; set; }

        public DbSet<MemberDto> Member { get; set; }

        public DbSet<MemberPropertyTypeDto> MemberPropertyType { get; set; }

        public DbSet<NodeDto> Node { get; set; }

        public DbSet<PropertyDataDto> PropertyData { get; set; }

        public DbSet<PropertyTypeCommonDto> PropertyTypeCommon { get; set; }

        public DbSet<PropertyTypeDto> PropertyType { get; set; }

        public DbSet<PropertyTypeGroupDto> PropertyTypeGroup { get; set; }

        public DbSet<PropertyTypeGroupReadOnlyDto> PropertyTypeGroupReadOnly { get; set; }

        public DbSet<PropertyTypeReadOnlyDto> PropertyTypeReadOnly { get; set; }

        public DbSet<RedirectUrlDto> RedirectUrl { get; set; }

        public DbSet<RelationDto> Relation { get; set; }

        public DbSet<RelationTypeDto> RelationType { get; set; }

        public DbSet<ServerRegistrationDto> ServerRegistration { get; set; }

        public DbSet<TagDto> Tag { get; set; }

        public DbSet<TagRelationshipDto> TagRelationship { get; set; }

        public DbSet<TemplateDto> Template { get; set; }

        public DbSet<User2NodeNotifyDto> User2NodeNotify { get; set; }

        public DbSet<User2UserGroupDto> User2UserGroup { get; set; }

        public DbSet<UserDto> User { get; set; }

        public DbSet<UserGroup2AppDto> UserGroup2App { get; set; }

        public DbSet<UserGroup2NodePermissionDto> UserGroup2NodePermission { get; set; }

        public DbSet<UserGroupDto> UserGroup { get; set; }

        public DbSet<UserLoginDto> UserLogin { get; set; }

        public DbSet<UserStartNodeDto> UserStartNode { get; set; }


        public UmbracoDbContext(DbContextOptions<UmbracoDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasSequence<int>("ContentTypeDto_seq", schema: "dbo").StartsAt(700).IncrementsBy(1);
            builder.HasSequence<int>("LanguageDto_seq", schema: "dbo").StartsAt(2).IncrementsBy(1);
            builder.HasSequence<int>("NodeDto_seq", schema: "dbo").StartsAt(NodeDto.NodeIdSeed).IncrementsBy(1);
            builder.HasSequence<int>("PropertyTypeDto_seq", schema: "dbo").StartsAt(100).IncrementsBy(1);
            builder.HasSequence<int>("PropertyTypeGroupDto_seq", schema: "dbo").StartsAt(56).IncrementsBy(1);
            builder.HasSequence<int>("RelationTypeDto_seq", schema: "dbo").StartsAt(RelationTypeDto.NodeIdSeed).IncrementsBy(1);
            builder.HasSequence<int>("UserGroupDto_seq", schema: "dbo").StartsAt(6).IncrementsBy(1);
        }
    }
}
