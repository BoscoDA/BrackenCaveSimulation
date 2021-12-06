using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Bat : Consumer, Guano
    {
        static Bat instance;
        private Bat()
        {

        }

        public static Bat GetInstance()
        {
            if (instance == null)
            {
                instance = new Bat();
            }
            return instance;
        }


        public override void Eat()
        {
            if (CornWorm.GetInstance().Population > 0 & CottonWorm.GetInstance().Population > 0)
            {
                    CornWorm.GetInstance().Population -= 21 * Population;
                    CottonWorm.GetInstance().Population -= 21 * Population;
            }
            else if (CottonWorm.GetInstance().Population == 0 & CornWorm.GetInstance().Population == 0)
            {
                Population -= 1;
            }
            else if (CornWorm.GetInstance().Population == 0)
            {
                CottonWorm.GetInstance().Population -= 42 * Population;
            }
            else if (CottonWorm.GetInstance().Population == 0)
            {
                CornWorm.GetInstance().Population -= 42 * Population;
            }
        }

        public override void Reproduce()
        {
            Population += Population;
        }

        public int ProduceGuano()
        {
            return Population * Utility.Probability.Next(20, 31);
        }
        public override bool CheckRatio()
        {
            if (CornWorm.GetInstance().Population + CottonWorm.GetInstance().Population > 42 * Population & Population > 0)
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