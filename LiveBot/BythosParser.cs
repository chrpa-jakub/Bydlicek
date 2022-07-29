using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LiveBot
{
    public class BythosParser : IParser
    {
        public Flat Parse(string url)
        {
            try
            {
                var doc = new HtmlDocument();
                var rawHtml = new WebClient().DownloadString(url);
                doc.LoadHtml(string.Join("\n", rawHtml.Split("\n")[255..]));
                var text = Regex.Replace(doc.DocumentNode.SelectSingleNode("//p").InnerHtml, "<br>", "\n");
                var locality = "Praha, " + text.Split("\n")[0];
                var size = text.Split("\n")[1].Split(",")[1].Trim();
                var priceRaw = text.Split("Nájem: ")[1].Split("\n")[0] == "dohodou"
                    ? "0"
                    : text.Split("Nájem: ")[1].Split("\n")[0];
                var price = int.Parse(Regex.Replace(priceRaw.Split(",")[0], @"\.|\s", ""));
                var name = $"Pronájem bytu {locality}";
                var link = "https://www.bythos.cz/#bytove-prostory-1";
                var bannedSizes = new List<string> {"1+1", "1+kk", "2+1", "2+kk", "3+kk"};
                var flatId = Sha256(locality);
                var isReserved = text.Split("\n")[^1].Trim().ToLower() == "rezervace";

                if (price > 22000 || bannedSizes.Contains(size) || isReserved)
                    return new Flat("-1", name, locality, new List<string>(), price, link, size);

                return new Flat(flatId, name, locality, new List<string>(), price, link, size);
            }
            catch
            {
                return new Flat("-1", "", "", new List<string>(), 0, "", "");
            }
        }

        static string Sha256(string randomString)
        {
            var hash = new StringBuilder();
            var crypto = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
                hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }
    }
}