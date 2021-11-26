using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Worm : Consumer
    {
        
        public int Metamorphosis()
        {
            int moths = Utility.Probability.Next(Population/2, Population);
            Population -= moths;
            return moths;
        }

        public override void Reproduce()
        {
            int moths = Metamorphosis();
            Population += moths*7;
        }
    }
}