namespace StevenVolckaert.VrtNu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProgramCategory
    {
        public string DisplayName { get; set; }
        public Uri Uri { get; set; }
        public Uri LowResolutionImageUri { get; set; }
        public Uri HighResolutionImageUri { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<Program> Programs { get; set; }
    }
}
