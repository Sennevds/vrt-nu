namespace StevenVolckaert.VrtNu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ProgramType Type { get; set; }
        public Uri Uri { get; set; }


        public ProgramCategory ProgramCategory { get; set; }
        public IEnumerable<ProgramSeason> Seasons { get; set; }
    }
}
