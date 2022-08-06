using System;

namespace LiveBot
{
    public class FlatStorage
    {
        public Flat Flat;
        public string OldId;
        
        public FlatStorage(int i, string link)
        {
            Flat = Flat.UseCorrectParser(i, link);
            OldId = Flat.Id;
        }
    }
}