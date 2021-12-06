using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CornCob : Item
    {
        static CornCob instance;

        private CornCob()
        {
            Name = "Corn Cob";
            Description = "A cob of delicious yellow sweet corn. Harvested locally in the Bracken Cave area of Texas.";
            Value = 0.95;
            PriceDetail += Value.ToString("c");
            Image = "../../media/corn.png";

        }

        public static CornCob GetInstance()
        {
            if (instance == null)
            {
                instance = new CornCob();
            }
            return instance;
        }
    }
}
