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

        private readonly (string Name, string JPathToUrl)[] _liveChannels =
            new (string, string)[]
            {
                ("Een", "$.vualto_een.rtsp"),
                ("Canvas", "$.vualto_canvas.rtsp"),
                ("Ketnet", "$.vualto_ketnet.rtsp"),
                ("Sporza", "$.vualto_sporza.rtsp"),
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
                       let url = jObject.SelectToken(x.JPathToUrl, errorWhenNoMatch: false)?.ToString()
                       where url != null
                       select new LiveChannel { Name = x.Name, Uri = new Uri(url) };
            }
        }
    }
}
