using System.Collections.Generic;
using System.Net;

namespace HarFileParser.Models
{
    public class Response
    {
        public HttpStatusCode Status { get; set; }

        public string StatusText { get; set; }

        public IEnumerable<Header> Headers { get; set; }

        public Content Content { get; set; }

        public string RedirectURL { get; set; }
    }
}