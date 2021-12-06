using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Corn : Producer
    {
        static Corn instance;
        
        private Corn()
        {
            
        }

        public static Corn GetInstance()
        {
            if (instance == null)
            {
                instance = new Corn();
            }
            return instance;
        }
        public override bool CheckRatio()
        {
            if (Environment.GetInstance().WaterSupply > 10)
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