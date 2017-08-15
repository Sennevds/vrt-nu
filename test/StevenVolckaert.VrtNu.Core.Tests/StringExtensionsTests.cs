namespace StevenVolckaert.VrtNu.Core.Tests
{
    using Xunit;

    public class StringExtensionsTests
    {
        [Fact]
        public void AsJsonStringTest()
        {
            var subject = @"FooBarBaz({""foo"": ""bar"", ""baz"": true})FooBar";

            Assert.Equal(
                expected: @"{""foo"": ""bar"", ""baz"": true}",
                actual: subject.AsJsonString()
            );
        }
    }
}
