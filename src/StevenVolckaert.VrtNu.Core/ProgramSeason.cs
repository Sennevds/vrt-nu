namespace StevenVolckaert.VrtNu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProgramSeason
    {
        public string DisplayName { get; set; }

        public Program Program { get; set; }
        public IEnumerable<ProgramEpisode> Episodes { get; set; }
    }
}
