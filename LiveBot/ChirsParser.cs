using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace LiveBot
{
    public class ChirsParser : IParser
    {
        public Flat Parse(string url)
        {
            try
            {
                var json = JObject.Parse(new WebClient().DownloadString(url));
                var flatInfo = json["squares"][1]["records"][0];
                var flatId = flatInfo["id"].ToObject<string>();
                var price = int.Parse(Regex.Replace(flatInfo["price"].ToObject<string>(), @"\s|Kč", ""));
                var name = flatInfo["title"].ToObject<string>();
                var size = name.Split(" ")[2].Split(",")[0];
                var locality = string.Join(",", name.Split(",")[3..]).Trim();
                var link = "https://www.chirs.cz/detail/" + flatId;
                var bannedSizes = new List<string> {"1+1", "1+kk", "2+1", "2+kk", "3+kk"};
                if (bannedSizes.Contains(size))
                    return new Flat("-1", name, locality, new List<string>(), price, link, size);

                return new Flat(flatId, name, locality, new List<string>(), price, link, size);
            }
            catch
            {
                return new Flat("-1", "", "", new List<string>(), 0, "", "");
            }
        }
    }
}