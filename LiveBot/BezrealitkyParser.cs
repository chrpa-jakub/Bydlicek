using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LiveBot
{
    public class BezrealitkyParser:IParser
    {
        public Flat Parse(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(new WebClient().DownloadString(url));
            var newest = doc.DocumentNode.SelectSingleNode("//article");
            doc.LoadHtml(newest.OuterHtml);
            var link = new Regex("href=\"(.*)\"").Matches(
                doc.DocumentNode.SelectSingleNode("//a").OuterHtml.Split(' ')[2])[0].Groups[1].ToString();
            var flatId = $"{link}".Split('/')[4].Split('-')[0];
            var locality = doc.DocumentNode.SelectSingleNode(
                "//span[@class='PropertyCard_propertyCardAddress__yzOdb text-subheadline text-truncate']").InnerHtml;
            var labels = doc.DocumentNode.SelectSingleNode("//p[@class='mt-2 mt-md-3 mb-0 text-caption text-truncate-multiple']").InnerHtml.Split('•').ToList();
            doc.LoadHtml(doc.DocumentNode.SelectSingleNode("//p[@class='PropertyPrice_propertyPrice__aJuok propertyPrice mb-0 mt-3']").InnerHtml);
            var prices = new List<int>
            {
                int.Parse(Regex.Replace(doc.DocumentNode.SelectSingleNode("//span").InnerHtml.Split("Kč")[0], @"\s+", "")),
                int.Parse(Regex.Replace(doc.DocumentNode.SelectSingleNode("//span[@class='PropertyPrice_propertyPriceAdditional__gMCQs']").InnerHtml.Split("Kč")[0], @"\+|\s+", ""))
            };
            doc.LoadHtml(newest.OuterHtml);
            var size = doc.DocumentNode.SelectSingleNode("//li[@class='FeaturesList_featuresListItem__SugGi']").InnerHtml.Split(">").Last();
            var name = $"Pronájem bytu {size}";

            return new Flat(flatId, name, locality, labels, prices.Sum(), link, size);
        }
    }
}