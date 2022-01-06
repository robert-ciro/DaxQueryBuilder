using System;
using System.Linq;
namespace DaxQueryBuilder.DaxFunctions
{
    public static class DaxFunctions
    {
        private static void CheckTableAndColumn(string table, string column)
        {
            CheckEmptyOrNull(table);
            CheckEmptyOrNull(column);
        }

        private static void CheckEmptyOrNull(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        public static string IsNotBlank(string table, string column)
        {
            CheckTableAndColumn(table, column);
            return $"NOT( ISBLANK( {table}[{column}] ) )";
        }

        public static string IsNotBlank(string measure) 
        {
            CheckEmptyOrNull(measure);
            return $"NOT( ISBLANK( [{measure}] ) )";
        }

        public static string IsEqual(string table, string column, string value)
        {
            CheckTableAndColumn(table, column);

            return $"TREATAS( {{\"{value}\"}}, {table}[{column}])";
        }

        public static string In(string table, string column, params string[] values)
        {
            CheckTableAndColumn(table, column);

            if (values != null && values.Length > 0)
            {
                var valuesConcatenatedbyComma = string.Join(", ", values.Select(value => "\"" + value + "\""));
                return $"TREATAS( {{{valuesConcatenatedbyComma}}}, {table}[{column}])";
            }

            return null;
        }

        public static string DateRange(string table, string column, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            CheckTableAndColumn(table, column);

            if (dateTimeFrom > dateTimeTo)
            {
                throw new ArgumentException("DateTimeFrom cannot be greater than dateTimeTo");
            }

            return $@"{table}[{column}] >= DATE({dateTimeFrom.Year},{dateTimeFrom.Month},{dateTimeFrom.Day}) && 
                      {table}[{column}] <= DATE({dateTimeTo.Year},{dateTimeTo.Month},{dateTimeTo.Day})";
        }

        public static string Between(string table, string column, int minValue, int maxValue)
        {
            CheckTableAndColumn(table, column);

            var value1 = Math.Min(minValue, maxValue);
            var value2 = Math.Max(minValue, maxValue);

            return $@"{table}[{column}] >= { value1 } && 
                      {table}[{column}] <= { value2 }";
        }
    }
}
