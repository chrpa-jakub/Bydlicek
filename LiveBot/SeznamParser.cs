using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace LiveBot
{
    public class SeznamParser : IParser
    {
        public Flat Parse(string url)
        {
            var parsed = JObject.Parse(new WebClient().DownloadString(url));
            var flatJson = parsed["_embedded"]["estates"][1];

            var flatId = flatJson["hash_id"].ToObject<string>();
            var name = flatJson["name"].ToObject<string>();
            var locality = flatJson["locality"].ToObject<string>();
            var labels = flatJson["labels"].ToObject<List<string>>();
            var price = flatJson["price"].ToObject<int>();

            var size = name.Split(' ')[2].Substring(0,4).Trim();
            var simpleLocality = flatJson["seo"]["locality"];
            var link = $@"https://www.sreality.cz/detail/pronajem/byt/{size}/{simpleLocality}/{flatId}";
            return new Flat(flatId, name, locality, labels, price, link, size);
        }
    }
}