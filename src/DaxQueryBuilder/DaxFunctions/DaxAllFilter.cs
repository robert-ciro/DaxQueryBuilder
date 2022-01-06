namespace DaxQueryBuilder.DaxFunctions
{
    public class DaxAllFilter : DaxFilterBase
    {
        public DaxAllFilter(string table) : base(table)
        {
            functionToApply = "All";
        }
    }
}
