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
                _ => throw new Exception("Wrong index.")
            };
        }
    }
}