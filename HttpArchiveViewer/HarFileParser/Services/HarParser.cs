using HarFileParser.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace HarFileParser.Services
{
    public class HarParser : IHarParser
    {
        public HarFile Parse(string rawData)
        {
            var file = new HarFile();
            var jsonData = JObject.Parse(rawData);
            var jsonPages = jsonData["log"]["pages"];
            var jsonEntries = jsonData["log"]["entries"];

            file.Pages = GetPages(jsonPages);
            file.Entries = GetEntries(jsonEntries);

            return file;
        }

        private IEnumerable<Page> GetPages(JToken jsonPages)
        {
            var pages = new List<Page>();

            foreach (var item in jsonPages)
            {
                var page = new Page();
                page.Id = GetStringValue(item, "id");
                page.Title = GetStringValue(item, "title");
                DateTime dateStarted;
                DateTime.TryParse(GetStringValue(item, "startedDateTime"), out dateStarted);
                page.DateStarted = dateStarted;
                double loadTime;
                double.TryParse(GetStringValue(item["pageTimings"], "onLoad"), out loadTime);
                page.LoadTime = loadTime;

                pages.Add(page);
            }

            return pages;
        }

        private IEnumerable<Entry> GetEntries(JToken jsonEntries)
        {
            var entries = new List<Entry>();

            foreach (var item in jsonEntries)
            {
                var entry = new Entry();
                DateTime dateStarted;
                DateTime.TryParse(GetStringValue(item, "startedDateTime"), out dateStarted);
                entry.DateStarted = dateStarted;
                double loadTime;
                double.TryParse(GetStringValue(item, "time"), out loadTime);
                entry.LoadTime = loadTime;
                entry.PageId = GetStringValue(item, "pageref");
                entry.ServerIP = GetStringValue(item, "serverIPAddress");
                entry.Request = item["request"].ToObject<Request>();
                entry.Response = item["response"].ToObject<Response>();

                entries.Add(entry);
            }

            return entries;
        }

        private string GetStringValue(JToken token, string key)
        {
            if (token == null)
            {
                return string.Empty;
            }

            return token.Value<string>(key);
        }
    }
}