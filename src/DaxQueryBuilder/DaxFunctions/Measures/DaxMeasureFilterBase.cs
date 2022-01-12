using System;

namespace DaxQueryBuilder.DaxFunctions.Measures
{
    public abstract class DaxMeasureFilterBase
    {
        public readonly string _functionToApply;
        public readonly string _table;

        protected DaxMeasureFilterBase(string table, string functionToApply)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _functionToApply = functionToApply ?? throw new ArgumentNullException(nameof(functionToApply));

        }

        public string IsNotBlank(string measure) => BuildFilter(DaxFunctions.IsNotBlank(measure));


        private string BuildFilter(string function) => $@"KEEPFILTERS(
                                                        FILTER({_functionToApply}({_table}),
                                                               {function}))";
    }
}
