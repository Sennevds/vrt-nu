namespace StevenVolckaert.VrtNu
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class VrtNuDataService : IVrtNuDataService
    {
        // use HtmlAgilityPack: 

        public async Task GetStuffAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.GetAsync(
                    requestUri: "");
            }
        }

        // TODO change to an array of tuples
        private readonly Dictionary<string, string> _liveChannels =
            new Dictionary<string, string>
            {
                { "Een", "vualto_een" },
                { "Canvas", "vualto_canvas"  },
                { "Ketnet", "vualto_ketnet" },
                { "Sporza", "vualto_sporza" },
            };

        private readonly Uri _liveChannelsUri = new Uri("https://services.vrt.be/videoplayer/r/live.json");

        public async Task<IEnumerable<LiveChannel>> GetLiveChannelsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_liveChannelsUri);
                var responseContent = await response.Content.ReadAsStringAsync();
                responseContent = responseContent.AsJsonString();
                var jObject = JObject.Parse(responseContent);

                return from x in _liveChannels
                       let url = jObject.SelectToken(x.Value)?.SelectToken("rtsp")?.ToString()
                       where url != null
                       select new LiveChannel { Name = x.Key, Uri = new Uri(url) };
            }
        }
    }
}
