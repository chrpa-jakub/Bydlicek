using System;

namespace LiveBot
{
    public class Values
    {
        public Flat Flat;
        public string OldId;
        
        public Values(int i, string[] links)
        {
            Flat = DecideParse(i, links[i]);
            OldId = Flat.Id;
        }
        
        public Flat DecideParse(int i, string link)
        {
            return i switch
            {
                0 => new BezrealitkyParser().Parse(link),
                1 => new SeznamParser().Parse(link),
                2 => new IdnesParser().Parse(link),
                3 => new CeskeRealityParser().Parse(link),
                4 => new RealityMixParser().Parse(link),
                5 => new RemaxParser().Parse(link),
                6 => new Reality18Parser().Parse(link),
                7 => new DumrealitParser().Parse(link),
                8 => new MaxiRealityParser().Parse(link),
                9 => new ChirsParser().Parse(link),
                10 => new MamParser().Parse(link),
                _ => throw new Exception("Wrong index.")
            };
        }
    }
}