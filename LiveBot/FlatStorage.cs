using System;

namespace LiveBot
{
    public class FlatStorage
    {
        public Flat Flat;
        public string OldId;
        
        public FlatStorage(Flat.Website website)
        {
            Flat = new Flat(website);
            OldId = Flat.Id;
        }
    }
}