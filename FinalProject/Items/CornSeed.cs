using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CornSeed : Item
    {
        static CornSeed instance;

        private CornSeed()
        {

        }

        public static CornSeed GetInstance()
        {
            if (instance == null)
            {
                instance = new CornSeed();
            }
            return instance;
        }

        public override void Effect(int quant)
        {
            Corn.GetInstance().Population += quant;
        }
    }
}
