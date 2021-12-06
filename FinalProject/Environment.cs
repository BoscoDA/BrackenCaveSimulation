using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Environment
    {
        #region Fields
        private string name = "Bracken Cave";
        private decimal temperature;
        private int guanoAmount;
        private int waterSupply = 100;
        private int days = 0;
        private Weather weatherSystem = new Weather();
        private List<Entity> entities;
        private string Weather;
        public event EventHandler<SetStatusEventArgs> SetStatus;
        enum Statuses
        {
            Balanced,
            Unbalanced,
            Unsound
        }
        private string status = Statuses.Balanced.ToString();

        public decimal Temperature { get => temperature; set => temperature = value; }
        public int GuanoAmount { get => guanoAmount; 
            set 
            {
                if (guanoAmount == value) return;
                if (value < 0) { guanoAmount = 0; return; }
                guanoAmount = value;
            } 
        }
        public int WaterSupply { get => waterSupply; set => waterSupply = value; }
        public int Days { get => days; set => days = value; }
        public List<Entity> Entities { get => entities; set => entities = value; }
        public string Name { get => name; set => name = value; }
        public Weather WeatherSystem { get => weatherSystem; set => weatherSystem = value; }
        public string Weather1 { get => Weather; set => Weather = value; }
        public string Status { get => status; set => status = value; }

        static Environment instance;
        #endregion
        private Environment()
        {
            entities = DataLoader.LoadEntities("../../data/Entities.xml");
        }
        public static Environment GetInstance()
        {
            if (instance == null)
            {
                instance = new Environment();
            }
            return instance;
        }
        public void ChangeWeather()
        {
            Weather = WeatherSystem.WeatherSystem();
        }
        public void CheckRatios()
        {
            int temp = 0;
            foreach (Entity e in entities)
            {
                if (e.Species != "Human" & e.Species != "Buteo jamaicensis" & e.Species != "Dermestes carnivora")
                {
                    if (e.CheckRatio() == true & e.Population > 0)
                    {
                        temp++;
                    }
                    else
                    {
                        temp--;
                    }
                }
            }
            SetStatus?.Invoke(this, new SetStatusEventArgs(temp));
        }
        public void Environment_SetStatus(object sender, SetStatusEventArgs e)
        {
            if (e.BalanceCount == 6)
            {
                Status = Statuses.Balanced.ToString();
            }
            else if (e.BalanceCount == 5)
            {
                Status = Statuses.Unbalanced.ToString();
            }
            else
            {
                Status = Statuses.Unsound.ToString();
            }
        }
        public class SetStatusEventArgs : EventArgs
        {
            public readonly int BalanceCount;

            public SetStatusEventArgs(int balanceCount)
            {
                BalanceCount = balanceCount;
            }
        }
    }
}