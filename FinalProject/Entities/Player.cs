using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Player : Person
    {
        static Player instance;
        private bool hasShovel = false;
        private bool hasOwl = false;

        public bool HasShovel { get => hasShovel; set => hasShovel = value; }
        public bool HasOwl { get => hasOwl; set => hasOwl = value; }

        private Player()
        {
        }

        public static Player GetInstance()
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }

    }
}