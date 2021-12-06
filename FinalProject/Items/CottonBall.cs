using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CottonBall : Item
    {
        static CottonBall instance;

        private CottonBall()
        {
            Name = "Cotton Ball";
            Description = "A fluffy white ball of cotton.";
            Value = 0.75;
        }

        public static CottonBall GetInstance()
        {
            if (instance == null)
            {
                instance = new CottonBall();
            }
            return instance;
        }
    }
}
