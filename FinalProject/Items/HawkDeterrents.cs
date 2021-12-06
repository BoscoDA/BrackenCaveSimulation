﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class HawkDeterrents : Item
    {

        static HawkDeterrents instance;

        private HawkDeterrents()
        {

        }

        public static HawkDeterrents GetInstance()
        {
            if (instance == null)
            {
                instance = new HawkDeterrents();
            }
            return instance;
        }
        public override void Effect(int quant)
        {
            Player.GetInstance().HasOwl = true;
        }
    }
}