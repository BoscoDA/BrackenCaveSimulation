using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Decomposer : Entity
    {
        //private int population;
        private int lifeSpan;
        private int age;

        //public int Population { get => population; set => population = value; }
        public int LifeSpan { get => lifeSpan; set => lifeSpan = value; }
        public int Age { get => age; set => age = value; }

        public virtual void Eat()
        {
            throw new System.NotImplementedException();
        }

        public void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}