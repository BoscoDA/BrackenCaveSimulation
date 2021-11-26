using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Entity
    {
        private string name;
        private string species;
        private int population;

        public string Status = "";
        public string Name { get => name; set => name = value; }
        public string Species { get => species; set => species = value; }
        public int Population { 
            get => population;
            set
            {
                if (population == value) return;
                if (value < 0) { population = 0; return; }
                decimal oldAmount = population;
                population = value;
                OnAmountChanged(new AmountChangedEventArgs(oldAmount, population));
            }
        }

        public event EventHandler<AmountChangedEventArgs> AmountChanged;

        protected virtual void OnAmountChanged(AmountChangedEventArgs e)
        {
            AmountChanged?.Invoke(this, e);
        }

        public void Entity_AmountChanged(object sender, AmountChangedEventArgs e)
        {
            if (e.LastAmount > e.NewAmount)
            {

                if (Species == "Tadarida brasiliensis")
                {
                    Status = "ALERT: Bat population is decreasing!";
                }
            }
        }

        public class AmountChangedEventArgs : EventArgs
        {
            public readonly decimal LastAmount;
            public readonly decimal NewAmount;

            public AmountChangedEventArgs(decimal lastAmount, decimal newAmount)
            {
                LastAmount = lastAmount; NewAmount = newAmount;
            }
        }
    }
}