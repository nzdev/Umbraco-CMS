using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EfCoreConverter
{
    class Program
    {
        private const string EntityConfigFolder = @"C:\Users\chadc\source\repos\V9\Umbraco-CMS\src\Umbraco.Infrastructure\Persistence\EfCore\EntityTypeConfigurations";

        static void Main(string[] args)
        {
            var dtoFolder = @"C:\Users\chadc\source\repos\V9\Umbraco-CMS\src\Umbraco.Infrastructure\Persistence\Dtos";
            var dtoFiles = new DirectoryInfo(dtoFolder).GetFiles();
            new DirectoryInfo(EntityConfigFolder).Create();

            foreach (var dtoFile in dtoFiles)
            {
                ProcessDtoFile(dtoFile);
            }

        }
        static void ProcessDtoFile(FileInfo file)
        {

            var configFolder = EntityConfigFolder;
            var sourceText = File.ReadAllText(file.FullName);
            var tree = CSharpSyntaxTree.ParseText(sourceText);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var modelCollector = new EfCoreModelConfigurationWalker();
            modelCollector.Visit(root);
            //Generate Config
            var configStr = GenerateEfDtoConfiguration(modelCollector.CurrentModel);
            File.WriteAllText(Path.Combine(EntityConfigFolder, $"{modelCollector.CurrentModel.DtoClassName}EntityTypeConfiguration.cs"), configStr);
            //Generate clean POCO
        }
        static string GenerateEfDtoConfiguration(ModelConfig model)
        {
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Umbraco.Cms.Infrastructure.Persistence.EfCore.EntityConfigurations")).NormalizeWhitespace();
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.EntityFrameworkCore")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.EntityFrameworkCore.Metadata.Builders")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(model.Namespace)));

            var classDeclaration = SyntaxFactory.ClassDeclaration($"{model.DtoClassName}EntityTypeConfiguration");
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.InternalKeyword));
            classDeclaration = classDeclaration.AddBaseListTypes(
               SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"IEntityTypeConfiguration<{model.DtoClassName}>")));

            // Create a stament with the body of a method.
            List<StatementSyntax> statements = GenerateEfConfigurationStatements(model);

            // Create a method
            var methodDeclaration = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Configure")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block(statements))
                .AddParameterListParameters(SyntaxFactory.ParseParameterList($"EntityTypeBuilder<{model.DtoClassName}> builder").Parameters.First());


            classDeclaration = classDeclaration.AddMembers(methodDeclaration);

            @namespace = @namespace.AddMembers(classDeclaration);

            // Normalize and get code as string.
            var code = @namespace
                .NormalizeWhitespace()
                .ToFullString();

            // Output new code to the console.
            Console.WriteLine(code);
            return code;
        }

        private static List<StatementSyntax> GenerateEfConfigurationStatements(ModelConfig model)
        {
            List<StatementSyntax> statements = new List<StatementSyntax>();

            statements.Add(SyntaxFactory.ParseStatement($"builder.ToTable({model.TableName});"));
            foreach (var prop in model.Properties)
            {
                if (prop.IsPrimaryKey)
                {
                    statements.Add(SyntaxFactory.ParseStatement($"builder.HasKey(x => x.{prop.PropertyName});"));
                    if (prop.PrimaryKeyAutoIncrement == "false")
                    {
                        statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).ValueGeneratedNever();"));
                    }
                }
                if (!string.IsNullOrWhiteSpace(prop.DbColumnName))
                {
                    statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasColumnName({prop.DbColumnName});"));
                }
                if (prop.IsForiegnKey)
                {
                    if (string.IsNullOrWhiteSpace(prop.ForiegnKeyDbName))
                    {
                        statements.Add(SyntaxFactory.ParseStatement($"builder.HasOne({prop.ForiegnKeyTypeName}).WithOne();"));

                    }
                    else
                    {
                        statements.Add(SyntaxFactory.ParseStatement($"builder.HasOne( {prop.ForiegnKeyTypeName}, {prop.ForiegnKeyDbName}).WithOne();"));
                    }

                }
                if (prop.IsResultColumn)
                {
                    if (prop.RefernceType == "ReferenceType.Many")
                    {
                        //1-M
                        statements.Add(SyntaxFactory.ParseStatement($"builder.HasMany(typeof({prop.PropertyType.Replace("List<", "").Replace(">", "")}), {prop.ReferenceMemberName});"));
                    }
                    else if (prop.RefernceType == "ReferenceType.OneToOne")
                    {
                        //1-M
                        statements.Add(SyntaxFactory.ParseStatement($"builder.HasOne(typeof({prop.PropertyType}),nameof({model.DtoClassName}.{prop.PropertyName}));"));
                    }
                    else
                    {

                    }
                }
                if (prop.Ignore)
                {
                    statements.Add(SyntaxFactory.ParseStatement($"builder.Ignore(x => x.{prop.PropertyName});"));
                }
                if(prop.NullSetting == "NullSettings.Null")
                {

                    statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).IsRequired(false);"));
                }
                else if (prop.NullSetting == "NullSettings.NotNull")
                {
                    statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).IsRequired(true);"));
                }
                if(!string.IsNullOrEmpty(prop.Length) && int.TryParse(prop.Length, out int length))
                {
                    statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasMaxLength(length);"));
                }

                if (prop.SpecialDbType != null)
                {
                    string dbType = null;
                    if("SpecialDbTypes.NTEXT" == prop.SpecialDbType)
                    {
                        dbType = "NTEXT";
                    }
                    else if ("SpecialDbTypes.NVARCHARMAX" == prop.SpecialDbType)
                    {
                        dbType = "nvarchar(max)";
                    }
                    else if ("SpecialDbTypes.NCHAR" == prop.SpecialDbType)
                    {
                        dbType = "nchar";
                    }
                    else
                    {

                    } 
                    statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasColumnType({dbType});"));
                }
                if (prop.Constraints.Any())
                {
                    foreach (var constraint in prop.Constraints)
                    {
                        if (!string.IsNullOrEmpty(constraint.Default))
                        {
                            if (constraint.Default == "SystemMethods.CurrentDateTime")
                            {
                                statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasDefaultValueSql(\"getdate()\");"));
                            }
                            else if (int.TryParse(constraint.Default, out int defaultInt))
                            {
                                if (!string.IsNullOrEmpty(constraint.Name))
                                {
                                    statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasDefaultValue({defaultInt});"));
                                }
                                statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasDefaultValue({defaultInt});"));
                            }
                            else if(!string.IsNullOrEmpty(constraint.Default) && int.TryParse(constraint.Default.Replace("\"",""), out int defaultIntS))
                            {
                                if (!string.IsNullOrEmpty(constraint.Name))
                                {
                                    statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasDefaultValue({defaultIntS});"));
                                }
                                statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasDefaultValue({defaultIntS});"));
                            }
                            else if(!string.IsNullOrEmpty(constraint.Default) && constraint.Default.Contains("\""))
                            {
                                statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasDefaultValueSql({constraint.Default});"));
                            }
                            else if (constraint.Default == "SystemMethods.NewGuid")
                            {
                                statements.Add(SyntaxFactory.ParseStatement($"builder.Property(x => x.{prop.PropertyName}).HasDefaultValueSql(\"NEWID()\");"));
                            }
                            else
                            {

                            }
                        }
                    }
                }
                if (prop.Indices.Any())
                {
                    foreach (var index in prop.Indices)
                    {

                        if (index.IndexType == "IndexTypes.UniqueNonClustered")
                        {
                            statements.Add(SyntaxFactory.ParseStatement($"builder.HasIndex(x => x.{prop.PropertyName}).IsUnique(true);"));
                        }
                        else if (index.IndexType == "IndexTypes.NonClustered")
                        {
                            statements.Add(SyntaxFactory.ParseStatement($"builder.HasIndex(x => x.{prop.PropertyName})"));
                        }
                        else
                        {

                        }
                    }
                }
            }
            return statements;
        }
    }
    class EfCoreModelConfigurationWalker : CSharpSyntaxWalker
    {
        public ModelConfig CurrentModel { get; set; }

        public EfCoreModelConfigurationWalker()
        {
            CurrentModel = new ModelConfig()
            {
                Properties = new List<ModelProperty>()
            };
        }
        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            CurrentModel.DtoClassName = node.Identifier.ValueText;
            CurrentModel.Namespace = "Umbraco.Cms.Infrastructure.Persistence.Dtos";
            var attrs = node.AttributeLists;
            if (!attrs.Any())
            {
                return;
            }
            foreach (var attrList in attrs)
            {
                foreach (var attr in attrList.Attributes)
                {
                    if (attr.Name.ToString() == "TableName")
                    {
                        var tableNameArg = GetArgument(attr, 0, "tableName");
                        CurrentModel.TableName = tableNameArg.Expression.ToString();
                    }
                    else if (attr.Name.ToString() == "PrimaryKey")
                    {
                        var primaryKeyIdArg = GetArgument(attr, 0, "primaryKey");
                        CurrentModel.PrimaryKeyColumnName = primaryKeyIdArg?.Expression?.ToString();
                        var primaryKeyAutoArg = GetArgument(attr, 1, "AutoIncrement");
                        CurrentModel.PrimaryKeyAutoIncremented = primaryKeyAutoArg?.Expression?.ToString();
                    }
                }
            }
            base.VisitClassDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            ModelProperty mp = new ModelProperty();
            mp.PropertyName = node.Identifier.ValueText;
            mp.PropertyType = node.Type.ToString();
            var attrs = node.AttributeLists;
            if (attrs.Any())
            {
                foreach (var attrList in attrs)
                {
                    foreach (var attr in attrList.Attributes)
                    {
                        if (attr.Name.ToString() == "Column")
                        {
                            var colmunNameArg = GetArgument(attr, 0, "name");
                            mp.DbColumnName = colmunNameArg.Expression.ToString();
                        }
                        else if (attr.Name.ToString() == "ForeignKey")
                        {
                            mp.IsForiegnKey = true;
                            var fkType = GetArgument(attr, 0, "type");
                            mp.ForiegnKeyTypeName = fkType?.Expression?.ToString();
                            var fkName = GetArgument(attr, 1, "Name");
                            mp.ForiegnKeyDbName = fkName?.Expression?.ToString();
                        }
                        else if (attr.Name.ToString() == "PrimaryKeyColumn")
                        {
                            mp.IsPrimaryKey = true;
                            var pkName = GetArgument(attr, 0, "Name");
                            mp.PrimaryKeyDbName = pkName?.Expression?.ToString();
                            var pkAuto = GetArgument(attr, 1, "AutoIncrement");
                            mp.PrimaryKeyAutoIncrement = pkAuto?.Expression?.ToString();
                        }
                        else if (attr.Name.ToString() == "Index")
                        {
                            ModelPropertyIndex mpi = new ModelPropertyIndex();
                            var idxType = GetArgument(attr, 0, "indexType");
                            mpi.IndexType = idxType?.Expression?.ToString();
                            var idxName = GetArgument(attr, 1, "Name");
                            mpi.IndexName = idxName?.Expression?.ToString();
                            mp.Indices.Add(mpi);
                        }
                        else if (attr.Name.ToString() == "ResultColumn")
                        {
                            mp.IsResultColumn = true;
                        }
                        else if (attr.Name.ToString() == "Reference")
                        {
                            var rt = GetArgument(attr, 0, "referenceType");
                            mp.RefernceType = rt?.Expression?.ToString();
                            var rn = GetArgument(attr, 1, "ReferenceMemberName");
                            mp.ReferenceMemberName = rn?.Expression?.ToString();
                        }
                        else if (attr.Name.ToString() == "Constraint")
                        {
                            PropertyConstraint mpc = new PropertyConstraint();
                            var defaultContraint = GetArgument(attr, 0, "Default");
                            mpc.Default = defaultContraint?.Expression?.ToString();
                            var nameContraint = GetArgument(attr, 1, "Name");
                            mpc.Name = nameContraint?.Expression?.ToString();
                            mp.Constraints.Add(mpc);
                        }
                        else if (attr.Name.ToString() == "NullSetting")
                        {
                            var nullSetting = GetArgument(attr, 0, "NullSetting");
                            mp.NullSetting = nullSetting?.Expression?.ToString();
                        }
                        else if (attr.Name.ToString() == "Length")
                        {
                            var length = GetArgument(attr, 0, "Length");
                            mp.Length = length?.Expression?.ToString();
                        }
                        else if (attr.Name.ToString() == "SpecialDbType")
                        {
                            var dbType = GetArgument(attr, 0, "databaseType");
                            mp.SpecialDbType = dbType?.Expression?.ToString();
                        }
                        else if (attr.Name.ToString() == "Ignore")
                        {
                          
                            mp.Ignore = true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            CurrentModel.Properties.Add(mp);
            base.VisitPropertyDeclaration(node);
        }
        private static AttributeArgumentSyntax GetArgument(AttributeSyntax attr, int index, string argName)
        {
            int count = 0;
            if (attr?.ArgumentList?.Arguments == null)
            {
                return null;
            }
            var byName = attr.ArgumentList.Arguments.FirstOrDefault(x => (x.NameColon != null && x.NameColon.GetText().ToString() == argName) || x.NameEquals != null && x.NameEquals.Name.Identifier.Text == argName);
            if(byName != null)
            {
                return byName;
            }
            return attr.ArgumentList.Arguments.FirstOrDefault(x => (x.NameColon == null && index == count++));
        }
    }
    public class ModelConfig
    {
        public string DtoClassName { get; set; }
        public string TableName { get; set; }
        public string PrimaryKeyColumnName { get; set; }
        public string PrimaryKeyAutoIncremented { get; set; }
        public int? PrimaryKeyInitialInt { get; set; }
        public List<ModelProperty> Properties { get; set; }
        public string Namespace { get; set; }

    }
    public class ModelProperty
    {
        public string PropertyName { get; set; }
        public string DbColumnName { get; set; }
        public string PropertyType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string PrimaryKeyDbName { get; set; }
        public string PrimaryKeyAutoIncrement { get; set; }
        public bool IsForiegnKey { get; set; }
        public string ForiegnKeyDbName { get; set; }
        public string ForiegnKeyTypeName { get; set; }
        public List<ModelPropertyIndex> Indices { get; set; } = new List<ModelPropertyIndex>();
        public bool IsResultColumn { get; set; }

        public string RefernceType { get; set; }
        public string ReferenceMemberName { get; set; }

        public List<PropertyConstraint> Constraints { get; set; } = new List<PropertyConstraint>();
        public string NullSetting { get; internal set; }
        public string Length { get; internal set; }
        public string SpecialDbType { get; internal set; }
        public bool Ignore { get; internal set; }
    }

    public class ModelPropertyIndex
    {
        public string IndexType { get; set; }
        public string IndexName { get; set; }
    }
    public class PropertyConstraint
    {
        public string Default { get; set; }
        public string Name { get; set; }
    }
}
