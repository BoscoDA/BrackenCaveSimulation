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
            throw new System.NotImplementedException();
        }

        public void Rain()
        {
            throw new System.NotImplementedException();
        }

        public void ClearSkies()
        {
            throw new System.NotImplementedException();
        }

        public void Thunderstorm()
        {
            throw new System.NotImplementedException();
        }

        public void Tornado()
        {
            throw new System.NotImplementedException();
        }

        public void Snow()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeTemperature()
        {
           Environment.GetInstance().Temperature = Utility.Probability.Next(68, 99);
        }

        public void WeatherSystem()
        {
            int prob = Utility.Probability.Next(0, 100);
            if (prob >=0 & prob < 50)
            {
                Rain();
            }
            else if(prob >=50 & prob < 75)
            {

            }
        }
    }
}