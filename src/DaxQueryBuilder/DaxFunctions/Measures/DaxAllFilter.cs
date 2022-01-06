namespace DaxQueryBuilder.DaxFunctions.Measures
{
    public class DaxAllFilter : DaxMeasureFilterBase
    {
        public DaxAllFilter(string table) : base(table, functionToApply: "All")
        {
        }
    }
}
