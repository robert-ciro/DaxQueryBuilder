namespace DaxQueryBuilder.DaxFunctions
{
    public class DaxValuesFilter : DaxFilterBase
    {
        public DaxValuesFilter(string table) : base(table)
        {
            functionToApply = "Values";
        }
    }
}
