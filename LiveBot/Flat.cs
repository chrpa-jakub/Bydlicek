using System;
using System.Collections.Generic;

namespace LiveBot
{
    public class Flat
    {
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

    }
}