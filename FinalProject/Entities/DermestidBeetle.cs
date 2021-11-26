using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class DermestidBeetle : Beetle
    {
        static DermestidBeetle instance;
        private DermestidBeetle()
        {
        }

        public static DermestidBeetle GetInstance()
        {
            if (instance == null)
            {
                instance = new DermestidBeetle();
            }
            return instance;
        }
        public override void Eat()
        {
            throw new System.NotImplementedException();
        }
    }
}