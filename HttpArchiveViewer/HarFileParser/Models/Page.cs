using System;

namespace HarFileParser.Models
{
    public class Page
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime DateStarted { get; set; }

        public double LoadTime { get; set; }
    }
}