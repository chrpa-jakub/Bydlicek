using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LiveBot
{
    public class Reality18Parser : IParser
    {
        public Flat Parse(string url)
        {
            var rawHtml = new WebClient().DownloadString(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(rawHtml);
            var size = doc.DocumentNode.SelectSingleNode("//div[@class='property-title']").InnerHtml.Split("\"")[3]
                .Split(" ")[2];
            if (size == "3+kk")
            {
                doc.LoadHtml(string.Join("", rawHtml.Split("\n")[415..]));
                size = doc.DocumentNode.SelectSingleNode("//div[@class='property-title']").InnerHtml.Split("\"")[3]
                    .Split(" ")[2];
            }

            var name = doc.DocumentNode.SelectSingleNode("//div[@class='property-title']").InnerHtml.Split("\"")[3];
            var locality = Regex.Replace(string.Join(",", name.Split(",")[3..]), @"&ndash;", "-").Trim();
            var price = int.Parse(Regex.Replace(
                doc.DocumentNode.SelectSingleNode("//span[@class='cena cena-pronajem']").InnerHtml.Trim().Split(" ")[0],
                @"&nbsp;", ""));
            var link = "https://www.reality18.cz" +
                       doc.DocumentNode.SelectSingleNode("//a[@class='property-link']").OuterHtml.Split("\"")[3];
            var flatId = link.Split("-")[^1].Split("/")[0];

            return new Flat(flatId, name, locality, new List<string>(), price, link, size);
        }
    }
}