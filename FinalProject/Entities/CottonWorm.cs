using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class CottonWorm : Worm
    {
        static CottonWorm instance;
        private CottonWorm()
        {
        }
        public static CottonWorm GetInstance()
        {
            if (instance == null)
            {
                instance = new CottonWorm();
            }
            return instance;
        }

        public override void Eat()
        {
            Cotton.GetInstance().Population -= 50 * (Population / 27000);
        }

    }
}