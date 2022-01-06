namespace DaxQueryBuilder.DaxFunctions.Measures
{
    public class DaxMeasureFilter
    {
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
