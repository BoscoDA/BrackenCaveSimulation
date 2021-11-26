using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Bat : Consumer
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

        public void ProduceGuano()
        {
            Environment.GetInstance().GuanoAmount += Population*Utility.Probability.Next(20, 31);
        }

        public override void Eat()
        {
            if (CornWorm.GetInstance().Population > 0 & CottonWorm.GetInstance().Population > 0)
            {
                for (int i = 0; i < Population; i++)
                {
                    CornWorm.GetInstance().Population -= Utility.Probability.Next(10, 21);
                    CottonWorm.GetInstance().Population -= Utility.Probability.Next(10, 21);
                }
            }
            else if (CornWorm.GetInstance().Population == 0)
            {
                CottonWorm.GetInstance().Population -= Utility.Probability.Next(10, 41);
            }
            else if (CottonWorm.GetInstance().Population == 0)
            {
                CornWorm.GetInstance().Population -= Utility.Probability.Next(10, 41);
            }
            else
            {
                Population -= 1;
            }
        }

        public override void Reproduce()
        {
            Population += Population;
        }
    }
}