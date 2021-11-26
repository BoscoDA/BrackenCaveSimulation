using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Environment
    {
        private string name = "Bracken Cave";
        private decimal temperature;
        private int guanoAmount;
        private int waterSupply = 100;
        private int days = 1;
        private Weather weatherSystem = new Weather();
        private List<Entity> entities;

        public decimal Temperature { get => temperature; set => temperature = value; }
        public int GuanoAmount { get => guanoAmount; set => guanoAmount = value; }
        public int WaterSupply { get => waterSupply; set => waterSupply = value; }
        public int Days { get => days; set => days = value; }
        public List<Entity> Entities { get => entities; set => entities = value; }
        public string Name { get => name; set => name = value; }
        public Weather WeatherSystem { get => weatherSystem; set => weatherSystem = value; }

        static Environment instance;
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
    }
}