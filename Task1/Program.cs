using System;
using Task1.BL;

namespace Task1
{

    class Program
    {   
        static void Main(string[] args)
        {
            ConsolManager cm = new ConsolManager();
            while (true)
            {
                cm.Walk(out String str);

                if (ConsolManager.isExite(out str)) return;
            }
        }
    }
}