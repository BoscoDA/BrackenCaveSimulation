using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Hawk : Consumer
    {
        static Hawk instance;
        private Hawk()
        {
        }

        public static Hawk GetInstance()
        {
            if (instance == null)
            {
                instance = new Hawk();
            }
            return instance;
        }
        public override void Eat()
        {
            Bat.GetInstance().Population -= Population;
        }

        public override void Reproduce()
        {
            throw new System.NotImplementedException();
        }
    }
}