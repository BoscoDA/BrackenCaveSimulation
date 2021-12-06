using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Person : Entity
    {
        private List<Item> inventory = new List<Item>();
        private double money;
        public List<Item> Inventory { get => inventory; set => inventory = value; }
        public double Money { get => money; set => money = value;}
        
    }
}