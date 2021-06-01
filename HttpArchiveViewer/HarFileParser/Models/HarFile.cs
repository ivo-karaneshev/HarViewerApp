using System.Collections.Generic;

namespace HarFileParser.Models
{
    public class HarFile
    {
        public IEnumerable<Page> Pages { get; set; }

        public IEnumerable<Entry> Entries { get; set; }
    }
}