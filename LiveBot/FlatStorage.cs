using System;

namespace LiveBot
{
    public class FlatStorage
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
        public Flat Flat;
        public string OldId;
        
        public FlatStorage(int i, string link)
        {
            Flat = UseCorrectParser(i, link);
            OldId = Flat.Id;
        }
        
        public Flat UseCorrectParser(int i, string link)
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