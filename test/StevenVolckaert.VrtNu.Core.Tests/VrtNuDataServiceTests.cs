namespace StevenVolckaert.VrtNu.Core.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class VrtNuDataServiceTests
    {
        [Theory]
        [InlineData(0, "Een", "https://live-w.lwc.vrtcdn.be/groupc/live/d05012c2-6a5d-49ff-a711-79b32684615b/live.isml/.m3u8")]
        [InlineData(1, "Canvas", "https://live-w.lwc.vrtcdn.be/groupc/live/905b0602-9719-4d14-ae2a-a9b459630653/live.isml/.m3u8")]
        [InlineData(2, "Ketnet", "https://live-w.lwc.vrtcdn.be/groupc/live/8b898c7d-adf7-4d44-ab82-b5bb3a069989/live.isml/.m3u8")]
        [InlineData(3, "Sporza", "https://live-w.lwc.vrtcdn.be/groupa/live/bf2f7c79-1d77-4cdc-80e8-47ae024f30ba/live.isml/.m3u8")]
        public async Task GetLiveChannelsAsync(int expectedPosition, string expectedChannelName, string expectedUrl)
        {
            IVrtNuDataService service = new VrtNuDataService();
            var channels = (await service.GetLiveChannelsAsync()).ToArray();

            if (channels.Length != 4)
                Assert.True(false, $"{channels.Length} channels were returned, while 4 were expected.");

            var channel = channels[expectedPosition];
            Assert.Equal(expectedChannelName, channel.Name);
            Assert.Equal(new Uri(expectedUrl), channel.Uri);
        }
    }
}
