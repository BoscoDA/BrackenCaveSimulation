using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Vender : Person
    {
        static Vender instance;
        Utility.ManipulateInventory Remove = Utility.RemoveItem;
        private Vender()
        {
            Inventory = DataLoader.LoadVenderInventory("../../data/Entities.xml");
            Money = 10000;
        }
        public static Vender GetInstance()
        {
            if (instance == null)
            {
                instance = new Vender();
            }
            return instance;
        }

        public string Buy(Item item, string quantity)
        {
            int quant = Convert.ToInt32(quantity);
            if (Player.GetInstance().Money >= item.Value * quant & quant > 0)
            {
                item.Effect(quant);
                if(item is Shovel || item is HawkDeterrents){ Remove(Inventory, item); }
                Player.GetInstance().Money -= item.Value * quant;
                return $"You purchase {quant} {item.Name} for ${item.Value*quant}\n";
            }
            else
            {
                return $"Transaction failed\n";
            }
        }
        public void Sell(Item item, string quantity)
        {
            int quant = Convert.ToInt32(quantity);
            item.Quantity -= quant;
            if(item.Quantity == 0)
            {
                Remove(Player.GetInstance().Inventory, item);
            }
            Player.GetInstance().Money += quant * item.Value;
        }
    }
}