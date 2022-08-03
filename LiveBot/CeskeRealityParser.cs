using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using HtmlAgilityPack;

namespace LiveBot
{
    public class CeskeRealityParser : IParser
    {
        public Flat Parse(string url)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var doc = new HtmlDocument();
                var encodedHtml = new WebClient {Encoding = Encoding.GetEncoding(1250)}.DownloadString(url);
                doc.LoadHtml(encodedHtml);
                var parsable = doc.DocumentNode.SelectSingleNode("//a[contains(@class,'nemo rel')]").OuterHtml;
                var link = parsable.Split("\"")[1];
                var name = Regex.Replace(parsable.Split('>')[1], "<sup", "") + "2";
                var locality = Regex.Replace(parsable.Split(">")[^2], @"(,\s)|</a", " ").Trim();
                var price = int.Parse(string.Join("",
                    new Regex(@"\d+").Matches(doc.DocumentNode.SelectSingleNode("//div[@class='cena']").InnerHtml)));
                var size = name.Split(' ')[2].Split(',')[0];
                var flatId = link.Split('/')[^1][4..];
                return new Flat(flatId, name, locality, new List<string>(), price, link, size);
            }
            catch
            {
                return new Flat("-1", "", "", new List<string>(), 0, "", "");
            }
        }
    }
}