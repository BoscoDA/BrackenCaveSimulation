using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Hawk : Consumer, Guano
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
            if(Player.GetInstance().HasOwl == false)
            {
                Bat.GetInstance().Population -= Population;
            }
            else
            {
                Bat.GetInstance().Population -= 0;
            }
            
        }

        public int ProduceGuano()
        {
            throw new NotImplementedException();
        }
    }
}