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
            Corn.GetInstance().Population -= 50*(Population/27000);
        }
    }
}