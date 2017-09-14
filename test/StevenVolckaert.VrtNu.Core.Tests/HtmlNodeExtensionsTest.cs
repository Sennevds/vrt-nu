namespace StevenVolckaert.VrtNu.Tests
{
    using System;
    using HtmlAgilityPack;
    using Xunit;

    public class HtmlNodeExtensionsTests
    {
        [Fact]
        public void GetImgSrcsetUrlsTest()
        {
            var htmlNode =
                HtmlNode.CreateNode(@"<img srcset=""//foo.com/bar/baz.jpg 1x, //foo.com/bar/baz.jpg 2x""/>");

            var actual = htmlNode.GetImgSrcsetUrls();

            Assert.Equal(2, actual.Length);
            Assert.Equal("//foo.com/bar/baz.jpg", actual[0]);
            Assert.Equal("//foo.com/bar/baz.jpg", actual[1]);
        }

        [Fact]
        public void GetImgSrcsetUrls_Returns_Empty_Array()
        {
            var htmlNode = HtmlNode.CreateNode(@"<img srcset="""">");

            var actual = htmlNode.GetImgSrcsetUrls();

            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetImgSrcsetUrls_With_Incorrect_Tag_Throws_ArgumentException()
        {
            var htmlNode = HtmlNode.CreateNode(@"<p>lorem ipsum</p>");

            var exception = Assert.Throws<ArgumentException>(() => htmlNode.GetImgSrcsetUrls());

            Assert.Equal(
                expected: "Argument represents a <p> tag, while a <img> tag was expected.\r\n"
                        + "Parameter name: htmlNode",
                actual: exception.Message
            );
        }

    }
}
