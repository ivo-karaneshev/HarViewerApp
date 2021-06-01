using HarFileParser.Models;

namespace HarFileParser.Services
{
    public interface IHarParser
    {
        HarFile Parse(string rawData);
    }
}