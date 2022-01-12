using System;
using System.Linq;
using DaxQueryBuilder.Extensions;

namespace DaxQueryBuilder
{ 
    public static class DaxQueryHelper
    {
        public static string ApplyFilterIn(string table, string column, params string[] values)
        {
            CheckTableAndColumn(table, column);

            if (values != null && values.Length > 0)
            {
                var valuesConcatenatedbyComma = string.Join(", ", values.Select(value => "\"" + value + "\""));
                return $"KEEPFILTERS(TREATAS( {{{valuesConcatenatedbyComma}}}, {table}[{column}]))";
            }

            return "";
        }

        public static string ApplyFilterIsEqual(string table, string column, string value)
        {
            CheckTableAndColumn(table, column);
            _ = value ?? throw new ArgumentNullException(nameof(value));

            return $"KEEPFILTERS(TREATAS( {{\"{value}\"}}, {table}[{column}]))";
        }

        private static void CheckTableAndColumn(string table, string column)
        {
            if (table.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ArgumentException("value with null, empty or white spaces not valid", nameof(table));
            }

            if (column.IsNullOrEmptyOrWhiteSpace())
            {
                throw new ArgumentException("value with null, empty or white spaces not valid", nameof(column));
            }
        }
    }
}
