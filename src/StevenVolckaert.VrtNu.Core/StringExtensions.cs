namespace StevenVolckaert.VrtNu
{
    /// <summary>
    ///     Provides extension methods for <see cref="string"/> values.
    /// </summary>
    public static class StringExtensions
    {
        public static string AsJsonString(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return value;

            var startIndex = value.IndexOf('{');
            var endIndex = value.LastIndexOf('}');

            return value.Substring(
                startIndex: startIndex,
                length: endIndex - startIndex + 1
            );
        }
    }
}
