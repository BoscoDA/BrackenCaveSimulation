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
            
            if (Cotton.GetInstance().Population > 0)
            {
                Cotton.GetInstance().Population -= 1;
            }
            else
            {
                Population -= 1000;
            }
        }
        public override bool CheckRatio()
        {
            if (Cotton.GetInstance().Population > 1 & Population > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}