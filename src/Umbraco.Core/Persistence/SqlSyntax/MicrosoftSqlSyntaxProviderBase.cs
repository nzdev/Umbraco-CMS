using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data;
using System.Linq;
using Umbraco.Core.Persistence.Querying;

namespace Umbraco.Core.Persistence.SqlSyntax
{
    /// <summary>
    /// Abstract class for defining MS sql implementations
    /// </summary>
    /// <typeparam name="TSyntax"></typeparam>
    public abstract class MicrosoftSqlSyntaxProviderBase<TSyntax> : SqlSyntaxProviderBase<TSyntax>
        where TSyntax : ISqlSyntaxProvider
    {
        protected MicrosoftSqlSyntaxProviderBase()
        {
            AutoIncrementDefinition = "IDENTITY(1,1)";
            GuidColumnDefinition = "UniqueIdentifier";
            RealColumnDefinition = "FLOAT";
            BoolColumnDefinition = "BIT";
            DecimalColumnDefinition = "DECIMAL(38,6)";
            TimeColumnDefinition = "TIME"; //SQLSERVER 2008+
            BlobColumnDefinition = "VARBINARY(MAX)";

            InitColumnTypeMap();
        }

        public override string RenameTable => "sp_rename '{0}', '{1}'";

        public override string AddColumn => "ALTER TABLE {0} ADD {1}";

        public override string GetQuotedTableName(string tableName)
        {
            if (tableName.Contains(".") == false)
                return $"[{tableName}]";

            var tableNameParts = tableName.Split(new[] { '.' }, 2);
            return $"[{tableNameParts[0]}].[{tableNameParts[1]}]";
        }

        public override string GetQuotedColumnName(string columnName) => $"[{columnName}]";

        public override string GetQuotedName(string name) => $"[{name}]";

        public override string GetStringColumnEqualComparison(string column, int paramIndex, TextColumnType columnType)
        {
            switch (columnType)
            {
                case TextColumnType.NVarchar:
                    return base.GetStringColumnEqualComparison(column, paramIndex, columnType);
                case TextColumnType.NText:
                    //MSSQL doesn't allow for = comparison with NText columns but allows this syntax
                    return $"{column} LIKE @{paramIndex}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(columnType));
            }
        }

        public override string GetStringColumnWildcardComparison(string column, int paramIndex, TextColumnType columnType)
        {
            switch (columnType)
            {
                case TextColumnType.NVarchar:
                    return base.GetStringColumnWildcardComparison(column, paramIndex, columnType);
                case TextColumnType.NText:
                    //MSSQL doesn't allow for upper methods with NText columns
                    return $"{column} LIKE @{paramIndex}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(columnType));
            }
        }

        /// <summary>
        /// This uses a the DbTypeMap created and custom mapping to resolve the SqlDataType
        /// </summary>
        /// <param name="clrType"></param>
        /// <returns></returns>
        public virtual SqlDataType GetSqlDataType(Type clrType)
        {
            var dbType = DbTypeMap.ColumnDbTypeMap.First(x => x.Key == clrType).Value;
            return GetSqlDataType(dbType);
        }

        /// <summary>
        /// Returns the mapped SqlDataType for the DbType specified
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public virtual SqlDataType GetSqlDataType(DbType dbType)
        {
            SqlDataType SqlDataType;

            //SEE: https://msdn.microsoft.com/en-us/library/cc716729(v=vs.110).aspx
            // and https://msdn.microsoft.com/en-us/library/yy6y35y8%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            switch (dbType)
            {
                case DbType.AnsiString:
                    SqlDataType = SqlDataType.VarChar;
                    break;
                case DbType.Binary:
                    SqlDataType = SqlDataType.VarBinary;
                    break;
                case DbType.Byte:
                    SqlDataType = SqlDataType.TinyInt;
                    break;
                case DbType.Boolean:
                    SqlDataType = SqlDataType.Bit;
                    break;
                case DbType.Currency:
                    SqlDataType = SqlDataType.Money;
                    break;
                case DbType.Date:
                    SqlDataType = SqlDataType.Date;
                    break;
                case DbType.DateTime:
                    SqlDataType = SqlDataType.DateTime;
                    break;
                case DbType.Decimal:
                    SqlDataType = SqlDataType.Decimal;
                    break;
                case DbType.Double:
                    SqlDataType = SqlDataType.Float;
                    break;
                case DbType.Guid:
                    SqlDataType = SqlDataType.UniqueIdentifier;
                    break;
                case DbType.Int16:
                    SqlDataType = SqlDataType.SmallInt;
                    break;
                case DbType.Int32:
                    SqlDataType = SqlDataType.Int;
                    break;
                case DbType.Int64:
                    SqlDataType = SqlDataType.BigInt;
                    break;
                case DbType.Object:
                    SqlDataType = SqlDataType.Variant;
                    break;
                case DbType.SByte:
                    throw new NotSupportedException("Inferring a SqlDataType from SByte is not supported.");
                case DbType.Single:
                    SqlDataType = SqlDataType.Real;
                    break;
                case DbType.String:
                    SqlDataType = SqlDataType.NVarChar;
                    break;
                case DbType.Time:
                    SqlDataType = SqlDataType.Time;
                    break;
                case DbType.UInt16:
                    throw new NotSupportedException("Inferring a SqlDataType from UInt16 is not supported.");
                case DbType.UInt32:
                    throw new NotSupportedException("Inferring a SqlDataType from UInt32 is not supported.");
                case DbType.UInt64:
                    throw new NotSupportedException("Inferring a SqlDataType from UInt64 is not supported.");
                case DbType.VarNumeric:
                    throw new NotSupportedException("Inferring a VarNumeric from UInt64 is not supported.");
                case DbType.AnsiStringFixedLength:
                    SqlDataType = SqlDataType.Char;
                    break;
                case DbType.StringFixedLength:
                    SqlDataType = SqlDataType.NChar;
                    break;
                case DbType.Xml:
                    SqlDataType = SqlDataType.Xml;
                    break;
                case DbType.DateTime2:
                    SqlDataType = SqlDataType.DateTime2;
                    break;
                case DbType.DateTimeOffset:
                    SqlDataType = SqlDataType.DateTimeOffset;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return SqlDataType;
        }
    }
}
