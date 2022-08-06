using System;
using System.Collections.Generic;
using System.Threading;

namespace LiveBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = new List<FlatStorage>();
            var emailer = new Emailer();
            for (var i = 0; i < Emailer.Links.Length; i++)
            {
                values.Add(new FlatStorage((Flat.Website)i));
            }

            Console.WriteLine("Bydlíček beží.");
            emailer.Loop(values);
        }
    }
}