namespace DaxQueryBuilder.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string value) =>
                 string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }
}
