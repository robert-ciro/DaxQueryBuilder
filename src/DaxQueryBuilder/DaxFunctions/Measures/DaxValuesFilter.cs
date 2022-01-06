namespace DaxQueryBuilder.DaxFunctions.Measures
{
    public class DaxValuesFilter : DaxMeasureFilterBase
    {
        public DaxValuesFilter(string table) : base(table, functionToApply: "Values")
        {
        }
    }
}
