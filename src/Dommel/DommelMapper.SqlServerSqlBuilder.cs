﻿using System;

namespace Dommel
{
    public static partial class DommelMapper
    {
        /// <summary>
        /// <see cref="ISqlBuilder"/> implementation for SQL Server.
        /// </summary>
        public class SqlServerSqlBuilder : ISqlBuilder
        {
            /// <inheritdoc/>
            public virtual string BuildInsert(Type type, string tableName, string[] columnNames, string[] paramNames) =>
                $"set nocount on insert into {tableName} ({string.Join(", ", columnNames)}) values ({string.Join(", ", paramNames)}); select scope_identity()";

            /// <inheritdoc/>
            public virtual string BuildPaging(string orderBy, int pageNumber, int pageSize)
            {
                var start = pageNumber >= 1 ? (pageNumber - 1) * pageSize : 0;
                return $" {orderBy} offset {start} rows fetch next {pageSize} rows only";
            }

            /// <inheritdoc/>
            public string PrefixParameter(string paramName) => $"@{paramName}";

            /// <inheritdoc/>
            public string QuoteIdentifier(string identifier) => $"[{identifier}]";
        }
    }
}
