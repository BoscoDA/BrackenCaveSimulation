using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Utility
    {
        public static Random Probability = new Random();
        public delegate void ManipulateInventory(List<Item> items, Item item);

        public static void RemoveItem(List<Item> items, Item item)
        {
            items.Remove(item);
        }

        public static void AddItem(List<Item> items, Item item)
        {
            items.Add(item);
        }
    }
}
