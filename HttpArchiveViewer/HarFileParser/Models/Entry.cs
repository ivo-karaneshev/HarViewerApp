using System;

namespace HarFileParser.Models
{
    public class Entry
    {
        public Request Request { get; set; }

        public Response Response { get; set; }

        public DateTime DateStarted { get; set; }

        public double LoadTime { get; set; }

        public string PageId { get; set; }

        public string ServerIP { get; set; }
    }
}