using HarFileParser.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HarFileParserTests
{
    [TestClass]
    public class HarParserTests
    {
        private const string _sampleHar = "{\"log\":{\"version\":\"1.2\",\"creator\":{\"name\":\"WebInspector\",\"version\":\"537.36\"},\"pages\":[{\"startedDateTime\":\"2013-08-24T20:16:16.997Z\",\"id\":\"page_1\",\"title\":\"http://ericduran.github.io/chromeHAR/\",\"pageTimings\":{\"onContentLoad\":317,\"onLoad\":406}}],\"entries\":[{\"startedDateTime\":\"2013-08-24T20:16:16.997Z\",\"time\":21,\"request\":{\"method\":\"GET\",\"url\":\"http://ericduran.github.io/chromeHAR/\",\"httpVersion\":\"HTTP/1.1\",\"headers\":[{\"name\":\"Pragma\",\"value\":\"no-cache\"},{\"name\":\"Accept-Encoding\",\"value\":\"gzip,deflate,sdch\"},{\"name\":\"Host\",\"value\":\"ericduran.github.io\"},{\"name\":\"Accept-Language\",\"value\":\"en-US,en;q=0.8\"},{\"name\":\"User-Agent\",\"value\":\"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.57 Safari/537.36\"},{\"name\":\"Accept\",\"value\":\"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\"},{\"name\":\"Cache-Control\",\"value\":\"no-cache\"},{\"name\":\"Cookie\",\"value\":\"_ga=GA1.2.1085478273.1366082592; __utma=145418720.1085478273.1366082592.1377368197.1377374929.19; __utmb=145418720.3.10.1377374929; __utmc=145418720; __utmz=145418720.1371696451.6.2.utmcsr=t.co|utmccn=(referral)|utmcmd=referral|utmcct=/6YNU3z0en1\"},{\"name\":\"Connection\",\"value\":\"keep-alive\"}],\"queryString\":[],\"cookies\":[{\"name\":\"_ga\",\"value\":\"GA1.2.1085478273.1366082592\",\"expires\":null,\"httpOnly\":false,\"secure\":false},{\"name\":\"__utma\",\"value\":\"145418720.1085478273.1366082592.1377368197.1377374929.19\",\"expires\":null,\"httpOnly\":false,\"secure\":false},{\"name\":\"__utmb\",\"value\":\"145418720.3.10.1377374929\",\"expires\":null,\"httpOnly\":false,\"secure\":false},{\"name\":\"__utmc\",\"value\":\"145418720\",\"expires\":null,\"httpOnly\":false,\"secure\":false},{\"name\":\"__utmz\",\"value\":\"145418720.1371696451.6.2.utmcsr=t.co|utmccn=(referral)|utmcmd=referral|utmcct=/6YNU3z0en1\",\"expires\":null,\"httpOnly\":false,\"secure\":false}],\"headersSize\":653,\"bodySize\":0},\"response\":{\"status\":200,\"statusText\":\"OK\",\"httpVersion\":\"HTTP/1.1\",\"headers\":[{\"name\":\"Date\",\"value\":\"Sat, 24 Aug 2013 20:16:17 GMT\"},{\"name\":\"Content-Encoding\",\"value\":\"gzip\"},{\"name\":\"Age\",\"value\":\"494\"},{\"name\":\"X-Cache\",\"value\":\"HIT\"},{\"name\":\"Connection\",\"value\":\"keep-alive\"},{\"name\":\"Content-Length\",\"value\":\"4991\"},{\"name\":\"X-Served-By\",\"value\":\"cache-v37-ASH\"},{\"name\":\"Last-Modified\",\"value\":\"Tue, 28 May 2013 14:53:55 GMT\"},{\"name\":\"Server\",\"value\":\"GitHub.com\"},{\"name\":\"X-Timer\",\"value\":\"S1377375377.587414742,VS0,VE0\"},{\"name\":\"Vary\",\"value\":\"Accept-Encoding\"},{\"name\":\"Content-Type\",\"value\":\"text/html\"},{\"name\":\"Via\",\"value\":\"1.1 varnish\"},{\"name\":\"Expires\",\"value\":\"Sat, 24 Aug 2013 20:18:03 GMT\"},{\"name\":\"Cache-Control\",\"value\":\"max-age=600\"},{\"name\":\"Accept-Ranges\",\"value\":\"bytes\"},{\"name\":\"X-Cache-Hits\",\"value\":\"2\"}],\"cookies\":[],\"content\":{\"size\":25800,\"mimeType\":\"text/html\",\"compression\":20809,\"text\":\"\"},\"redirectURL\":\"\",\"headersSize\":457,\"bodySize\":4991},\"cache\":{},\"timings\":{\"blocked\":0,\"dns\":0,\"connect\":-1,\"send\":0,\"wait\":19,\"receive\":1,\"ssl\":0},\"pageref\":\"page_1\"}]}}";

        // Example test for parsing .har files
        [TestMethod]
        public void ExampleTest()
        {
            var parser = new HarParser();
            var result = parser.Parse(_sampleHar);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Pages.Count() == 1);
            Assert.IsTrue(result.Entries.Count() == 1);
        }
    }
}
