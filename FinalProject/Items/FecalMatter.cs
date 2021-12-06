using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class FecalMatter : Item
    {
        static FecalMatter instance;

        private FecalMatter()
        {
            Name = "Bat Guano";
            Description = "A disgusting pile of bat guano. It reeks and doesn't do much for you. Can be sold for money, but keep in mind that the beetles native to the cave rely on it for food.";
            Value = 0.50;
            PriceDetail += Value.ToString("c");
            Image = "../../media/bat.png";

        }

        public static FecalMatter GetInstance()
        {
            if (instance == null)
            {
                instance = new FecalMatter();
            }
            return instance;
        }
    }
}