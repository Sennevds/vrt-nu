namespace StevenVolckaert.VrtNu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using Newtonsoft.Json.Linq;

    public class VrtNuDataService : IVrtNuDataService
    {
        // HtmlAgilityPack makes use of XPath; see https://www.w3schools.com/xml/xpath_syntax.asp for info.

        private const string MainContentSelector = @"//main[@id='main-content']";

        private static readonly Uri _baseUri = new Uri("https://www.vrt.be", uriKind: UriKind.Absolute);
        private static readonly Uri _allProgramsUri = new Uri(_baseUri, "vrtnu/a-z/");
        private static readonly Uri _allProgramCategoriesUri = new Uri(_baseUri, "vrtnu/categorieen/");
        // _VRTNU_SEARCH_URL = "https://search.vrt.be/suggest?facets[categories]="

        private const string AllProgramNodesSelector = @".//div[@class='vrtglossary__groups']//div[@class='vrtglossary__list']/ul/li";
        private const string AllProgramHyperlinkSelector = @"./a[@class='tile']";
        private const string AllProgramTitleSelector = @".//h3[@class='tile__title']/text()";
        private const string AllProgramDescriptionSelector = @".//div[@class='tile__description']/p";

        private const string CategoryNodesSelector = @"//div[@class='page-category page']";
        private const string CategoryHyperlinkSelector = @"./a[@class='tile tile--category']";
        private const string CategoryTitleSelector = @".//h3[@class='tile__title']";
        private const string CategoryImageSelector = @".//div[@class='tile__image']/picture/img";

        public async Task<IEnumerable<Program>> GetAllProgramsAsync()
        {
            var programsUrl = _allProgramsUri.ToString();

            // TODO Add logging. Steven volckaert. September 14, 2017.

            var htmldocument = await new HtmlWeb().LoadFromWebAsync(programsUrl);
            var contentNode = htmldocument.DocumentNode.SelectSingleNode(MainContentSelector);

            if (contentNode == null)
                throw new HtmlWebException(
                    $"HTML document at {programsUrl} contains no matching elements " +
                    $"for XPath expression \"{MainContentSelector}\"."
                );

            //var pageTitle = contentNode.SelectSingleNode("h1")?.InnerText;
            // get all nodes that match @"./a[@class='tile']"
            // see https://github.com/stevenvolckaert/plugin.video.vrt.nu/blob/master/resources/lib/vrtplayer/vrtplayer.py#L152

            var programNodes = contentNode.SelectNodes(AllProgramNodesSelector);

            var programs =
                from x in programNodes
                let url = x.SelectSingleNode(AllProgramHyperlinkSelector)?.Attributes["href"]?.Value
                where url != null
                let name = x.SelectSingleNode(AllProgramTitleSelector)?.InnerHtml
                let description = x.SelectSingleNode(AllProgramDescriptionSelector)?.InnerText
                // TODO select more attributes
                select new Program
                {
                    DisplayName = name,
                    // TODO Process text: Replace many white space with a single space char.
                    Description = description,
                    //Type = ,
                    Uri = new Uri(_baseUri, url)
                };

            return programs.ToList();
        }

        public async Task<IEnumerable<ProgramCategory>> GetAllProgramCategoriesAsync()
        {
            var categoriesUrl = _allProgramCategoriesUri.ToString();

            // TODO Add logging. Steven Volckaert. September 4, 2017.

            var htmlDocument = await new HtmlWeb().LoadFromWebAsync(categoriesUrl);
            var contentNode = htmlDocument.DocumentNode.SelectSingleNode(MainContentSelector);

            if (contentNode == null)
                throw new HtmlWebException(
                    $"HTML document at {categoriesUrl} contains no matching elements " +
                    $"for XPath expression \"{MainContentSelector}\"."
                );

            var categoryNodes = contentNode.SelectNodes(CategoryNodesSelector);

            if (categoryNodes == null)
                throw new HtmlWebException(
                    $"HTML document at {categoriesUrl} contains no matching elements " +
                    $"for XPath expression \"{CategoryNodesSelector}\"."
                );

            // TODO select imageUri
            // https://developer.xamarin.com/guides/xamarin-forms/user-interface/images/

            var programCategories =
                from x in categoryNodes
                let url = x.SelectSingleNode(CategoryHyperlinkSelector)?.Attributes["href"]?.Value
                where url != null
                let name = x.SelectSingleNode(CategoryTitleSelector)?.InnerHtml
                let imageUris = ParseCategoryImageSrcset(x)
                select new ProgramCategory
                {
                    DisplayName = name,
                    Uri = new Uri(_baseUri, url),
                    LowResolutionImageUri = imageUris.lowResolutionImageUri,
                    HighResolutionImageUri = imageUris.highResolutionImageUri,
                };

            return programCategories.ToList();
        }

        private (Uri lowResolutionImageUri, Uri highResolutionImageUri) ParseCategoryImageSrcset(
            HtmlNode htmlNode
        )
        {
            if (htmlNode == null)
                return (null, null);

            var imageNode = htmlNode.SelectSingleNode(CategoryImageSelector);

            if (imageNode == null)
                return (null, null);

            var imageUris = imageNode.GetImgSrcsetUrls().Select(x => new Uri($"https:{x}")).ToArray();

            if (imageUris == null || imageUris.IsEmpty())
                return (null, null);

            return imageUris.Length > 1
                ? (imageUris[0], imageUris[1])
                : (imageUris[0], null);
        }

        public async Task<IEnumerable<ProgramEpisode>> GetAllProgramEpisodes(Program program)
        {
            throw new NotImplementedException();
        }

        private readonly (string Name, string JPathToUrl)[] _liveChannels =
            new(string, string)[]
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
                       select new LiveChannel
                       {
                           Name = x.Name,
                           Uri = new Uri(url)
                       };
            }
        }
    }
}
