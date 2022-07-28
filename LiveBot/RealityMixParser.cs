using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LiveBot
{
    public class RealityMixParser : IParser
    {
        public Flat Parse(string url)
        {
            var doc = new HtmlDocument();
            var rawHtml = new WebClient().DownloadString(url);
            doc.LoadHtml(rawHtml);
            if(rawHtml.Contains("TIP regionu"))
            {
                doc.LoadHtml(string.Join("",rawHtml.Split("\n")[350..]));
            }
            var link = doc.DocumentNode.SelectSingleNode("//a[@class='advert-list-items__images']").OuterHtml.Split("\"")[1];
            var name = Regex.Replace(doc.DocumentNode.SelectSingleNode("//h2").InnerHtml.Split(">")[^2],@"</a|\s{2,}","");
            var locality = doc.DocumentNode.SelectSingleNode("//p[@class='advert-list-items__content-address']")
                .InnerHtml;
            var price = int.Parse(Regex.Replace(doc.DocumentNode.SelectSingleNode("//span[@class='advert-list-items__content-price-price']")
                .InnerHtml,@"\s|Kč",""));
            var flatId = Regex.Replace(link.Split("-")[^1],@"\.html","");
            var size = name.Split(",")[1][1..];
            return new Flat(flatId, name, locality, new List<string>(), price, link, size);
        }
    }
}