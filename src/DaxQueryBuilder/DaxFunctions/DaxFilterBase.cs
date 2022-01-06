using System;

namespace DaxQueryBuilder.DaxFunctions
{
    public abstract class DaxFilterBase
    {
        protected string functionToApply;
        protected readonly string _table;

        protected DaxFilterBase(string table)
        {
            _table = table;
        }

        public string IsNotBlank(string column) => BuildFilter(column, DaxFunctions.IsNotBlank(_table, column));

        public string DateRange(string column, DateTime from, DateTime to) => BuildFilter(column, DaxFunctions.DateRange(_table, column, from, to));

        public string Between(string column, int minValue, int maxValue) => BuildFilter(column, DaxFunctions.Between(_table, column, minValue, maxValue));

        private string BuildFilter(string column, string function) => $@"KEEPFILTERS(
                                                        FILTER({functionToApply}({_table}[{column}]),
                                                               {function}))";
    }
}
