using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LiveBot
{
    public class MamParser : IParser
    {
        public Flat Parse(string url)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(new WebClient().DownloadString(url));
                var name = doc.DocumentNode.SelectSingleNode("//a[contains(@class,'fw-normal')]").InnerHtml.Trim();
                var link =
                    doc.DocumentNode.SelectSingleNode("//a[contains(@class,'fw-normal')]").OuterHtml.Split("\"")[3];
                var flatId = link.Split('/')[^2];
                var size = name.Split(" ")[2].Split(",")[0];
                var priceRaw = doc.DocumentNode
                    .SelectSingleNode("//strong[@class='block text-secondary fs-xxl pl-4 pr-4 mb-4']").InnerHtml
                    .Split(" ");
                var price = int.Parse(priceRaw[0] + priceRaw[1]);
                var locality = doc.DocumentNode.SelectSingleNode("//p[@class='fs-s fw-light pl-4 pr-4']").InnerHtml
                    .Split("<br>")[0].Trim();
                return new Flat(flatId, name, locality, new List<string>(), price, link, size);
            }
            catch
            {
                return new Flat("-1", "", "", new List<string>(), 0, "", "");
            }
        }
    }
}