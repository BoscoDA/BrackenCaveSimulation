﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Cotton : Producer
    {
        static Cotton instance;
        private Cotton()
        {
            
        }

        public static Cotton GetInstance()
        {
            if (instance == null)
            {
                instance = new Cotton();
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