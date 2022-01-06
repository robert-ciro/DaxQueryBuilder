using DaxQueryBuilder.DaxFunctions;

namespace DaxQueryBuilder
{
    public class DaxFilter
    {
        public string In(string table, string column, params string[] values)
        {
            return DaxQueryHelper.ApplyFilterIn(table, column, values);
        }

        public string IsEqual(string table, string column, string value)
        {
            return DaxQueryHelper.ApplyFilterIsEqual(table, column, value);
        }

        public DaxAllFilter All(string table)
        {
            var daxFunctions = new DaxAllFilter(table);
            return daxFunctions;
        }

        public DaxValuesFilter Values(string table)
        {
            var daxFunctions = new DaxValuesFilter(table);
            return daxFunctions;
        }
    }
}
