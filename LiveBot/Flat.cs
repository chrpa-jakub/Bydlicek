using System;
using System.Collections.Generic;

namespace LiveBot
{
    public class Flat
    {
        private enum Website
        {
            Bezrealitky,
            Seznam,
            Idnes,
            CeskeReality,
            RealityMix,
            Remax,
            Reality18,
            Dumrealit,
            MaxiReality,
            Chirs,
            Mam,
            Bythos
        }
        public string Id;
        public string Name;
        public string Locality;
        public List<string> Labels;
        public int Price;
        public string Link;
        public string Size;

        public Flat(string id, string name, string locality, List<string> labels, int price, string link, string size)
        {
            Id = id;
            Name = name;
            Locality = locality;
            Labels = labels;
            Price = price;
            Link = link;
            Size = size;
        }

        public string GetRaw()
        {
            return $"{Name} {Locality} {Price} {Size}";
        }

        public static Flat UseCorrectParser(int i, string link)
        {
            return (Website)i switch
            {
                Website.Bezrealitky => new BezrealitkyParser().Parse(link),
                Website.Seznam => new SeznamParser().Parse(link),
                Website.Idnes => new IdnesParser().Parse(link),
                Website.CeskeReality => new CeskeRealityParser().Parse(link),
                Website.RealityMix => new RealityMixParser().Parse(link),
                Website.Remax => new RemaxParser().Parse(link),
                Website.Reality18 => new Reality18Parser().Parse(link),
                Website.Dumrealit => new DumrealitParser().Parse(link),
                Website.MaxiReality => new MaxiRealityParser().Parse(link),
                Website.Chirs => new ChirsParser().Parse(link),
                Website.Mam => new MamParser().Parse(link),
                Website.Bythos => new BythosParser().Parse(link),
                _ => throw new Exception("Wrong index.")
            };
        }
    }
}