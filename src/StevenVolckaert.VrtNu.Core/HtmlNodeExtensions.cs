namespace StevenVolckaert.VrtNu
{
    using System;
    using HtmlAgilityPack;
    using System.Linq;

    /// <summary>
    ///     Provides extension methods for <see cref="HtmlNode"/> instances.
    /// </summary>
    public static class HtmlNodeExtensions
    {
        /// <summary>
        ///     Returns an array of URLs contained in the srcset attribute of the node,
        ///     provided that it represents a HTML &lt;img&gt; tag.
        /// </summary>
        /// <param name="htmlNode">
        ///     The <see cref="HtmlNode"/> instance this extension method affects.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="htmlNode"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="htmlNode"/> doesn't represent a HTML &lt;img&gt; tag.
        /// </exception>
        public static string[] GetImgSrcsetUrls(this HtmlNode htmlNode)
        {
            if (htmlNode == null)
                throw new ArgumentNullException(nameof(htmlNode));

            if (htmlNode.Name != "img")
                throw new ArgumentException(
                    message: $"Argument represents a <{htmlNode.Name}> tag, while a <img> tag was expected.",
                    paramName: nameof(htmlNode)
                );

            var srcset = htmlNode.Attributes["srcset"]?.Value;

            if (srcset.IsNullOrWhiteSpace())
                return new string[] { };

            var q = srcset.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.FirstFromSplit(" ") ?? x)
                .Where(x => x.IsNullOrWhiteSpace() == false)
                .ToArray();

            return q;
        }

        // TODO Create unit test in project StevenVolckaert.VrtNu.Core.Tests.
        private static bool HasClass(this HtmlNode element, string className)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (className.IsNullOrWhiteSpace())
                throw new ArgumentException(
                    message: Resources.ValueNullEmptyOrWhiteSpace,
                    paramName: nameof(className)
                );

            if (element.NodeType != HtmlNodeType.Element)
                return false;

            return ClassListContains(element.Attributes["class"], className, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Performs optionally-whitespace-padded string search without new string allocations.
        /// </summary>
        /// <remarks>
        ///     A regex might work as well, however constructing a new <see cref="Regex"/> instance every time
        ///     this method is called is expensive.
        /// </remarks>
        private static bool ClassListContains(
            HtmlAttribute htmlAttribute,
            string className,
            StringComparison stringComparison
        )
        {
            if (htmlAttribute == null)
                return false;

            var attribute = htmlAttribute.Value;

            if (string.Equals(attribute, className, stringComparison))
                return true;

            var index = 0;

            while (index + className.Length <= attribute.Length)
            {
                index = attribute.IndexOf(className, index, stringComparison);
                if (index == -1)
                    return false;

                var end = index + className.Length;

                // Needle must be enclosed in whitespace or be at the start/end of string
                var validStart = index == 0 || Char.IsWhiteSpace(attribute[index - 1]);
                var validEnd = end == attribute.Length || Char.IsWhiteSpace(attribute[end]);

                if (validStart && validEnd)
                    return true;

                index++;
            }

            return false;
        }
    }
}
