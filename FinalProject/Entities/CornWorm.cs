using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class CornWorm : Worm
    {
        static CornWorm instance;
        private CornWorm()
        {
            
        }
        public static CornWorm GetInstance()
        {
            if (instance == null)
            {
                instance = new CornWorm();
            }
            return instance;
        }

        public override void Eat()
        {
            if(Corn.GetInstance().Population > 0)
            {
                Corn.GetInstance().Population -= 1;
            }
            else
            {
                Population -= 1000;
            }
            
        }
        public override bool CheckRatio()
        {
            if (Corn.GetInstance().Population > 1 & Population > 0)
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