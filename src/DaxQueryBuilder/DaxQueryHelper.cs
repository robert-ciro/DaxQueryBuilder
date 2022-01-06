using System;
using System.Linq;

namespace DaxQueryBuilder
{ 
    public static class DaxQueryHelper
    {
        public static string ApplyFilterIn(string table , string column, params string[] values)
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

            return $"KEEPFILTERS(TREATAS( {{\"{value}\"}}, {table}[{column}]))";
        }

        private static void CheckTableAndColumn(string table, string column)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException(nameof(table));
            }

            if (string.IsNullOrEmpty(column))
            {
                throw new ArgumentNullException(nameof(column));
            }
        }
    }
}
