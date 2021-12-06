using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Item
    {
        private string name;
        private double value;
        private string description;
        private string image;
        private string priceDetail = $"Price: ";
        private int quantity;

        public string Name { get => name; set => name = value; }
        public double Value { get => value; set => this.value = value; }
        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }
        public string PriceDetail { get => priceDetail; set => priceDetail = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public virtual void Effect(int quant)
        {
        }
    }
}