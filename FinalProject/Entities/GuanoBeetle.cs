using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class GuanoBeetle : Beetle
    {

        static GuanoBeetle instance;
        private GuanoBeetle()
        {

        }

        public static GuanoBeetle GetInstance()
        {
            if (instance == null)
            {
                instance = new GuanoBeetle();
            }
            return instance;
        }
        public override void Eat()
        {
            if(Environment.GetInstance().GuanoAmount > 0)
            {
                Environment.GetInstance().GuanoAmount -= 3 * Population; ;
            }
            else
            {
                Population -= 10;
            }
            
        }
        public override bool CheckRatio()
        {
            if(Environment.GetInstance().GuanoAmount > 3 * Population & Population > 0)
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