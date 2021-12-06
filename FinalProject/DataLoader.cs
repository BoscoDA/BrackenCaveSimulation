using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace FinalProject
{
    class DataLoader
    {
        /* 
        Example code provided in class (PROG 201)
        Expects base class: Entity
        Expects derived classes from Entity: Producer, Consumer, Decomposer, Person
        */
        public static List<Entity> LoadEntities(string fileName)
        {
            List<Entity> entities = new List<Entity>();
            if (File.Exists(fileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode root = doc.DocumentElement;
                XmlNodeList entityList = root.SelectNodes("/environment/entity");
                foreach (XmlElement entity in entityList)
                {
                    Entity temp;
                    if (entity.GetAttribute("type") == "Producer")
                    {
                        if(entity.GetAttribute("name") == "Corn")
                        {
                            Corn corn = Corn.GetInstance();
                            temp = corn;
                        }
                        else
                        {
                            Cotton cotton = Cotton.GetInstance();
                            temp = cotton;
                        }
                    }
                    else if (entity.GetAttribute("type") == "Consumer")
                    {
                        if (entity.GetAttribute("name") == "Corn Earworm")
                        {
                            CornWorm cornWorm = CornWorm.GetInstance();
                            temp = cornWorm;
                        }
                        else if (entity.GetAttribute("name") == "Cotton Bollworm")
                        {
                            CottonWorm cottonWorm = CottonWorm.GetInstance();
                            temp = cottonWorm;
                        }
                        else if (entity.GetAttribute("name") == "Brazilian Free-Tailed Bat")
                        {
                            Bat bat = Bat.GetInstance();
                            temp = bat;
                        }
                        else
                        {
                            Hawk hawk = Hawk.GetInstance();
                            temp = hawk;
                        }
                        
                    }
                    else if (entity.GetAttribute("type") == "Decomposer")
                    {
                        if(entity.GetAttribute("name") == "Guano Beetle")
                        {
                            GuanoBeetle guanoBeetle = GuanoBeetle.GetInstance();
                            temp = guanoBeetle;
                        }
                        else
                        {
                            DermestidBeetle dermestidBeetle = DermestidBeetle.GetInstance();
                            temp = dermestidBeetle;
                        }
                    }
                    else if (entity.GetAttribute("species") == "Human")
                    {
                        if (entity.GetAttribute("type") == "vender")
                        {
                            Vender vender = Vender.GetInstance();
                            temp = vender;
                        }
                        else
                        {
                            Person person = Vender.GetInstance();
                            temp = person;
                        }
                    }
                    else
                    {
                        temp = new Entity();
                    }
                    temp.Name = entity.GetAttribute("name");
                    temp.Species = entity.GetAttribute("species");

                    temp.Population = Convert.ToInt32(entity.GetAttribute("population"));


                    entities.Add(temp);
                }
            }
            return entities;
        }

        public static List<Item> LoadVenderInventory(string fileName)
        {
            List<Item> inventory = new List<Item>();
            if (File.Exists(fileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode root = doc.DocumentElement;
                XmlNodeList entityList = root.SelectNodes("/environment/entity");
                XmlNodeList itemList;

                foreach (XmlElement entity in entityList)
                {
                    itemList = entity.ChildNodes;
                    
                    foreach (XmlElement item in itemList)
                    {
                        Item temp;
                        if (item.GetAttribute("name") == "Corn Seed")
                        {
                            temp = CornSeed.GetInstance();
                        }
                        else if (item.GetAttribute("name") == "Cotton Seed")
                        {
                            temp = CottonSeed.GetInstance();
                        }
                        else if (item.GetAttribute("name") == "Owl Decoy")
                        {
                            temp = HawkDeterrents.GetInstance();
                        }
                        else if (item.GetAttribute("name") == "Shovel")
                        {
                            temp = Shovel.GetInstance();
                        }
                        else
                        {
                            Item i = new Item();
                            temp = i;
                        }
                        
                        temp.Name = item.GetAttribute("name");
                        temp.Value = Convert.ToDouble(item.GetAttribute("value"));
                        temp.PriceDetail += temp.Value.ToString("c");
                        temp.Description = item.GetAttribute("description");
                        temp.Image = item.GetAttribute("picture");
                        temp.Quantity = Convert.ToInt32(item.GetAttribute("quantity"));
                        inventory.Add(temp);
                    }
                }
            }
            return inventory;
        }
    }
}
