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
            switch (i)
            {
                case 0:
                    return new BezrealitkyParser().Parse(link);
                case 1:
                    return new SeznamParser().Parse(link);
                case 2:
                    return new IdnesParser().Parse(link);
                case 3:
                    return new CeskeRealityParser().Parse(link);
                case 4:
                    return new RealityMixParser().Parse(link);
                case 5:
                    return new RemaxParser().Parse(link);
            }

            throw new Exception("Wrong index");
        }
    }
}