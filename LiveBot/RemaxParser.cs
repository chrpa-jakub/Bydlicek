using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LiveBot
{
    public class RemaxParser : IParser
    {
        public Flat Parse(string url)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(new WebClient().DownloadString(url));
                var link = "https://www.remax-czech.cz" +
                           doc.DocumentNode.SelectSingleNode("//a[@class='pl-items__link']").OuterHtml.Split("\"")[3];
                var flatId = link.Split("/")[5];
                var name = Regex.Replace(doc.DocumentNode.SelectSingleNode("//h2[@class='h5']").InnerHtml.Split(">")[1],
                    "</strong", "");
                var size = name.Split(' ')[2];
                var locality = Regex.Replace(doc.DocumentNode.SelectSingleNode("//p").InnerHtml, @"\s{2,}|&ndash;", "");
                var price = int.Parse(Regex.Replace(
                    doc.DocumentNode.SelectSingleNode("//div[@class='pl-items__item-price']").InnerHtml.Split('>')[1],
                    @"\s|Kč|<small", ""));
                return new Flat(flatId, name, locality, new List<string>(), price, link, size);
            }
            catch
            {
                return new Flat("-1", "", "", new List<string>(), 0, "", "");
            }
        }
    }
}