using System.Collections.Generic;

namespace HarFileParser.Models
{
    public class Request
    {
        public string Method { get; set; }

        public string URL { get; set; }

        public IEnumerable<Header> Headers { get; set; }

        public IEnumerable<QueryParameter> QueryString { get; set; }
    }
}