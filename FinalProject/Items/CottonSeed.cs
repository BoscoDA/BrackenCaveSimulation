using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CottonSeed : Item
    {
        static CottonSeed instance;

        private CottonSeed()
        {

        }

        public static CottonSeed GetInstance()
        {
            if (instance == null)
            {
                instance = new CottonSeed();
            }
            return instance;
        }

        public override void Effect(int quant)
        {
            Cotton.GetInstance().Population += quant;
        }
    }
}
