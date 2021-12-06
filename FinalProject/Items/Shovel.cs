using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Shovel : Item
    {
        static Shovel instance;

        private Shovel()
        {

        }

        public static Shovel GetInstance()
        {
            if (instance == null)
            {
                instance = new Shovel();
            }
            return instance;
        }

        public override void Effect(int quant)
        {
            Player.GetInstance().HasShovel = true;
        }
    }
}
