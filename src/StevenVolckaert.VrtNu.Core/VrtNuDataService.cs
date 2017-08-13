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
            // TODO Get URLs from https://services.vrt.be/videoplayer/r/live.json, parse with NewtonSoft.JSON.

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_liveChannelsUri);
                var responseContent = await response.Content.ReadAsStringAsync();
                responseContent = responseContent.AsJsonString();
                var jObject = JObject.Parse(responseContent);

                var channels = from x in _liveChannels
                               let url = jObject.SelectToken(x.Value)?.SelectToken("rtsp").ToString()
                               where url != null
                               select new LiveChannel { Name = x.Key, Uri = new Uri(url) };

                //var een = jObject.SelectToken("vualto_een")?.SelectToken("rtsp");
                ////int totalPages = (int)jObject.SelectToken("total_pages");
                //var canvas = jObject.SelectToken("vualto_canvas")?.SelectToken("rtsp");

                return channels;
            }

            //    var liveChannels = from x in _liveChannels
            //                       select new LiveChannel { Name = x.Key, Uri = x.Value };

            //return liveChannels;
        }
    }
}
