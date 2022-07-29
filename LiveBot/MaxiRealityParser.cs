using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LiveBot
{
    public class MaxiRealityParser : IParser
    {
        public Flat Parse(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(new WebClient().DownloadString(url));
            var name = Regex.Replace(doc.DocumentNode.SelectSingleNode("//a[@class='text-red']").InnerHtml,"&sup2;","2");
            var size = name.Split(" ")[2];
            var locality = doc.DocumentNode.SelectSingleNode("//p[@class='fs-14 no-margin-bottom adress']").InnerHtml;
            var priceRaw = doc.DocumentNode.SelectSingleNode("//span[@class='big text-blue']").InnerHtml.Split(" ");
            var link = "https://www.maxirealitypraha.cz"+doc.DocumentNode.SelectSingleNode("//a[@class='text-red']").OuterHtml.Split("\"")[1];
            var price = int.Parse(priceRaw[0] + priceRaw[1]);
            var flatId = Regex.Replace(link.Split("-")[^1],@"\.html","");

            return new Flat(flatId, name, locality, new List<string>(), price, link, size);
        }
    }
}