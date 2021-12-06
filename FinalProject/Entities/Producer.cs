using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Producer : Entity
    {
        public Producer()
        {
            
        }
        public void GatherNutrients()
        {
            if(Environment.GetInstance().WaterSupply > 10 & Population > 0)
            {
                Environment.GetInstance().WaterSupply -= 10;
            }
            else
            {
                Population -= 1000;
            }
        }

    }
}