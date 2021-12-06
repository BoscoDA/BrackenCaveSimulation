using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Weather
    {

        public void Drought()
        {
            Environment.GetInstance().WaterSupply = 0;
        }

        public void Rain()
        {
            Environment.GetInstance().WaterSupply += Utility.Probability.Next(15,55);
            ChangeTemperature();
        }

        public void ClearSkies()
        {
            ChangeTemperature();
        }

        public void Thunderstorm()
        {
            Corn.GetInstance().Population -= Utility.Probability.Next(100, 200);
            Cotton.GetInstance().Population -= Utility.Probability.Next(100, 200);
            Environment.GetInstance().WaterSupply += Utility.Probability.Next(15, 55);
        }

        public void Tornado()
        {
            Corn.GetInstance().Population -= Utility.Probability.Next(500, 1000);
            Cotton.GetInstance().Population -= Utility.Probability.Next(500, 1000);
        }

        public void ChangeTemperature()
        {
            Environment.GetInstance().Temperature = Utility.Probability.Next(68, 99);
        }

        public string WeatherSystem()
        {
            int prob = Utility.Probability.Next(0, 100);
            if (prob >= 0 & prob < 50)
            {
                ClearSkies();
                return "Clear Skies";
            }
            else if (prob >= 50 & prob < 80)
            {
                Rain();
                return "Rain";
            }
            else if (prob >= 80 & prob < 90)
            {
                Thunderstorm();
                return "Thunderstorm";
            }
            else if (prob >= 92 & prob < 95)
            {
                Drought();
                return "Drought";
            }
            else if (prob >= 95 & prob < 100)
            {
                Tornado();
                return "Tornado";
            }
            else { return ""; }
            
        }
    }
}