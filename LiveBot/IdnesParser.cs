using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LiveBot
{
    public class IdnesParser : IParser
    {
        public Flat Parse(string url)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(new WebClient().DownloadString(url));
                var name = "Pronájem " + doc.DocumentNode.SelectSingleNode("//h2[@class='c-products__title']").InnerHtml
                    .Split('>').Last().Substring(1);
                var size = name.Split(' ')[2].Trim();
                var price = int.Parse(Regex.Replace(
                    doc.DocumentNode.SelectSingleNode("//p[@class='c-products__price']").InnerHtml.Split('>')[1],
                    @"(\s+|Kč/měsíc</strong)", ""));
                var locality =
                    Regex.Replace(doc.DocumentNode.SelectSingleNode("//p[@class='c-products__info']").InnerHtml,
                        @"\s{2,}", "").Trim();
                var link = new Regex("href=\"(.*)\" d").Matches(doc.DocumentNode
                    .SelectSingleNode("//a[@class='c-products__link']").OuterHtml)[0].Groups[1].ToString();
                var flatId = link.Split('/')[^2];
                return new Flat(flatId, name, locality, new List<string>(), price, link, size);
            }
            catch
            {
                return new Flat("-1", "", "", new List<string>(), 0, "", "");
            }
        }
    }
}