using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace LiveBot
{
    public class DumrealitParser : IParser
    {
        public Flat Parse(string url)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(new WebClient().DownloadString(url));
                var name = doc.DocumentNode.SelectSingleNode("//h3").InnerHtml.Split("<")[0];
                var size = name.Split(",")[1].Trim();
                var locality = doc.DocumentNode.SelectSingleNode("//p").InnerHtml;
                var priceRaw = doc.DocumentNode.SelectSingleNode("//span[contains(@class,'price')]").InnerHtml
                    .Split(" ");
                var flatId = doc.DocumentNode.SelectSingleNode("//span[contains(@class,'id')]").InnerHtml.Split(" ")[1];
                var link = "https://www.dumrealit.cz" +
                           doc.DocumentNode.SelectSingleNode("//a[contains(@class,'hovout')]").OuterHtml.Split("\"")[1];
                var price = int.Parse(priceRaw[0] + priceRaw[1]);
                return new Flat(flatId, name, locality, new List<string>(), price, link, size);
            }
            catch
            {
                return new Flat("-1", "", "", new List<string>(), 0, "", "");
            }
        }
    }
}