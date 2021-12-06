using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for Ecosystem.xaml
    /// </summary>
    public partial class Ecosystem : Page
    {
        #region Fields
        Environment environment = Environment.GetInstance();
        DispatcherTimer timer;
        TimeSpan timeSpan;
        public bool gamePause = false;
        int shopBuyIndex = 0;
        int shopSellIndex = 0;
        #endregion
        public Ecosystem()
        {
            InitializeComponent();
        }

        #region LoadInfo
        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInformation();
            if (environment.Days == 1)
            {
                State.Content = "Start";
            }
            else { State.Content = "Resume"; }

            CurrentDay.Text = $"Day: {environment.Days}/273";
        }
        private void LoadInformation()
        {
            LoadEntityNames();
            LoadEntityPopulation();
            LoadEnvironmentInformation();
            environment.SetStatus += environment.Environment_SetStatus;
            foreach (Entity e in environment.Entities)
            {
                //only update player about the entities that aren't the player and vendor
                if (e.Species.ToLower() != "human")
                {
                    UpdatePlayerOnRatios(e);
                }
            }
        }
        private void LoadEntityNames()
        {
            CornName.DataContext = Corn.GetInstance();
            CottonName.DataContext = Cotton.GetInstance();
            BatName.DataContext = Bat.GetInstance();
            HawkName.DataContext = Hawk.GetInstance();
            CornWormName.DataContext = CornWorm.GetInstance();
            CottonWormName.DataContext = CottonWorm.GetInstance();
            GuanoBeetleName.DataContext = GuanoBeetle.GetInstance();
            DermestidBeetleName.DataContext = DermestidBeetle.GetInstance();
        }
        private void LoadEntityPopulation()
        {
            CornPopulation.Text = Corn.GetInstance().Population.ToString();
            CottonPopulation.Text = Cotton.GetInstance().Population.ToString();
            BatPopulation.Text = Bat.GetInstance().Population.ToString();
            HawkPopulation.Text = Hawk.GetInstance().Population.ToString();
            CornWormPopulation.Text = CornWorm.GetInstance().Population.ToString();
            CottonWormPopulation.Text = CottonWorm.GetInstance().Population.ToString();
            GuanoBeetlePopulation.Text = GuanoBeetle.GetInstance().Population.ToString();
            DermestidBeetlePopulation.Text = DermestidBeetle.GetInstance().Population.ToString();
        }
        private void LoadEnvironmentInformation()
        {
            EnvironmentName.Text = environment.Name;
            EnvironmentGuanoAmountOuput.Text = $"Guano Amount: {environment.GuanoAmount}";
            EnvironmentTemperatureOuput.Text = $"Temperature: {environment.Temperature} °F";
            EnvironmentWaterSupplyOuput.Text = $"Water Supply: {environment.WaterSupply}%";
            Weather.Text = $"Conditions: {environment.Weather1}";
            EcosystemStatus.Text = environment.Status;
            environment.CheckRatios();
        }
        private void LoadShop()
        {
            LoadShopBuy();
            LoadShopSell();

        }
        private void LoadShopBuy()
        {
            BuyQuantity.Text = "0";
            TotalPrice.Text = "$0.00";
            if (Vender.GetInstance().Inventory.Count == 0)
            {
                ShopBuyGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                ShopBuyGrid.DataContext = Vender.GetInstance().Inventory[shopBuyIndex];
            }
        }
        private void LoadShopSell()
        {
            SellQuant.Text = "0";
            TotalPriceSell.Text = "$0.00";
            PlayerMoneyOutput.Text = $"Player Money: " + Player.GetInstance().Money.ToString("C");
            if (Player.GetInstance().Inventory.Count == 0)
            {
                ShopSellGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                ShopSellGrid.Visibility = Visibility.Visible;
                ShopSellGrid.DataContext = Player.GetInstance().Inventory[shopSellIndex];
            }

        }
        private void LoadHarvestMenu()
        {
            CornHarvestName.DataContext = Corn.GetInstance();
            CornHarvestPopulation.Text = Corn.GetInstance().Population.ToString();
            CornHarvestQuant.Text = "0";

            CottonHarvestName.DataContext = Cotton.GetInstance();
            CottonHarvestPopulation.Text = Cotton.GetInstance().Population.ToString();
            CottonHarvestQuant.Text = "0";

            GuanoHarvestName.Text = "Guano";
            GuanoHarvestPopulation.Text = Environment.GetInstance().GuanoAmount.ToString();
            GuanoHarvestQuant.Text = "0";
            if(Player.GetInstance().HasShovel == false)
            {
                GuanoHarvestDecreaseQuant.Visibility = Visibility.Hidden;
                GuanoHarvestQuant.Visibility = Visibility.Hidden;
                GuanoHarvestIncreaseQuant.Visibility = Visibility.Hidden;
                ShovelNeeded.Visibility = Visibility.Visible;
                Quantity.Visibility = Visibility.Hidden;
            }
            else
            {
                GuanoHarvestDecreaseQuant.Visibility = Visibility.Visible;
                GuanoHarvestQuant.Visibility = Visibility.Visible;
                GuanoHarvestIncreaseQuant.Visibility = Visibility.Visible;
                ShovelNeeded.Visibility = Visibility.Hidden;
                Quantity.Visibility = Visibility.Visible;
            }
        }
        #endregion

        private void GameLoop()
        {
            environment.Days++;
            if (environment.Days <= 273 & gamePause == false)
            {
                Bat.GetInstance().Eat();
                environment.GuanoAmount += Bat.GetInstance().ProduceGuano();
                GuanoBeetle.GetInstance().Eat();
                environment.WeatherSystem.ChangeTemperature();
                environment.ChangeWeather();
                Corn.GetInstance().GatherNutrients();
                Cotton.GetInstance().GatherNutrients();
                Hawk.GetInstance().Eat();
                CornWorm.GetInstance().Eat();
                CottonWorm.GetInstance().Eat();
                if (environment.Days % 90 == 0)
                {
                    CornWorm.GetInstance().Reproduce();
                    CottonWorm.GetInstance().Reproduce();
                }
                else if (environment.Days == 135)
                {
                    Bat.GetInstance().Reproduce();
                }
                LoadInformation();
                Counter();
            }

        }
        private void Counter()
        {
            //DispatchTimer example by kmatyaszek (https://stackoverflow.com/users/1410998/kmatyaszek)
            timeSpan = TimeSpan.FromSeconds(1);

            timer = new DispatcherTimer(
                new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal,
                delegate
                {
                    //txtTimer.Text = timeSpan.ToString("c");
                    CurrentDay.Text = $"Day: {environment.Days}/273";
                    if (timeSpan == TimeSpan.Zero)
                    {
                        timer.Stop();
                        //call the game loop here
                        GameLoop();
                    }
                    timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                },
                Application.Current.Dispatcher);

            timer.Start();
        }
        private void HarvestCorn()
        {
            if (Convert.ToInt32(CornHarvestQuant.Text) > 0)
            {
                int cornLoss = Convert.ToInt32(CornHarvestQuant.Text);
                CornCob.GetInstance().Quantity += cornLoss;
                Player.GetInstance().Inventory.Add(CornCob.GetInstance());
                Corn.GetInstance().Population -= cornLoss;
            }
        }
        private void HarvestCotton()
        {
            if (Convert.ToInt32(CottonHarvestQuant.Text) > 0)
            {
                int cottonLoss = Convert.ToInt32(CottonHarvestQuant.Text);
                CottonBall.GetInstance().Quantity += cottonLoss;
                Player.GetInstance().Inventory.Add(CottonBall.GetInstance());
                Cotton.GetInstance().Population -= cottonLoss;
            }
        }
        private void HarvestGuano()
        {
            if (Convert.ToInt32(GuanoHarvestQuant.Text) > 0)
            {
                int guanoLoss = Convert.ToInt32(GuanoHarvestQuant.Text);
                FecalMatter.GetInstance().Quantity += guanoLoss;
                Player.GetInstance().Inventory.Add(FecalMatter.GetInstance());
                environment.GuanoAmount -= guanoLoss;
            }
        }

        #region Buttons
        private void State_Click(object sender, RoutedEventArgs e)
        {
            if (State.Content.ToString() == "Start")
            {
                State.Content = "Pause";
                gamePause = false;
                EventOutput.Content = "~ Simulation Started ~";
                Counter();
            }
            else if (State.Content.ToString() == "Pause")
            {
                gamePause = true;
                State.Content = "Resume";
            }
            else
            {
                gamePause = false;
                State.Content = "Pause";
                Counter();
            }
        }
        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            if (Shop.Content.ToString() == "Shop")
            {
                ShopWindow.Visibility = Visibility.Visible;
                Shop.Content = "Ecosystem";
                State.Visibility = Visibility.Hidden;
                HarvestGrid.Visibility = Visibility.Hidden;
                HarvestMenu.Content = "Harvest";
                LoadShop();
            }
            else
            {
                ShopWindow.Visibility = Visibility.Hidden;
                Shop.Content = "Shop";
                State.Visibility = Visibility.Visible;
                LoadInformation();
            }
        }
        private void NextItem_Click(object sender, RoutedEventArgs e)
        {
            shopBuyIndex++;
            if (shopBuyIndex == Vender.GetInstance().Inventory.Count)
            {
                shopBuyIndex = 0;
            }
            LoadShop();
        }
        private void PreviousItem_Click(object sender, RoutedEventArgs e)
        {
            shopBuyIndex--;
            if (shopBuyIndex <= 0)
            {
                shopBuyIndex = Vender.GetInstance().Inventory.Count - 1;
            }
            LoadShop();
        }
        private void IncreaseQuant_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(BuyQuantity.Text) + 1;
            if (quant > Vender.GetInstance().Inventory[shopSellIndex].Quantity)
            {
                quant = 0;
            }
            BuyQuantity.Text = quant.ToString();
            TotalPrice.Text = (quant * Vender.GetInstance().Inventory[shopBuyIndex].Value).ToString("c");
        }
        private void DecreaseQuant_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(BuyQuantity.Text) - 1;
            if (quant < 0)
            {
                quant = Convert.ToInt32(Player.GetInstance().Money / Vender.GetInstance().Inventory[shopBuyIndex].Value);
                if(quant > Vender.GetInstance().Inventory[shopBuyIndex].Quantity)
                {
                    quant = Vender.GetInstance().Inventory[shopBuyIndex].Quantity;
                }
                BuyQuantity.Text = quant.ToString();
            }
            BuyQuantity.Text = quant.ToString();
            TotalPrice.Text = (quant * Vender.GetInstance().Inventory[shopBuyIndex].Value).ToString("c");
        }
        private void Harvest_Click(object sender, RoutedEventArgs e)
        {
            HarvestCorn();
            HarvestCotton();
            HarvestGuano();
            
            LoadHarvestMenu();
        }
        private void HarvestMenu_Click(object sender, RoutedEventArgs e)
        {
            if (HarvestMenu.Content.ToString() == "Harvest")
            {
                ShopWindow.Visibility = Visibility.Hidden;
                Shop.Content = "Shop";
                HarvestGrid.Visibility = Visibility.Visible;
                LoadHarvestMenu();
                HarvestMenu.Content = "Ecosystem";
                State.Visibility = Visibility.Hidden;
            }
            else
            {
                HarvestGrid.Visibility = Visibility.Hidden;
                HarvestMenu.Content = "Harvest";
                State.Visibility = Visibility.Visible;
                LoadInformation();
            }
        }
        private void CornHarvestIncreaseQuant_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(CornHarvestQuant.Text) + 1;
            if(quant > Corn.GetInstance().Population)
            {
                quant = 0;
            }
            CornHarvestQuant.Text = quant.ToString();
            CornHarvestQuant.Text = quant.ToString();
        }
        private void CottonHarvestIncreaseQuant_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(CottonHarvestQuant.Text) + 1;
            if (quant > Cotton.GetInstance().Population)
            {
                quant = 0;
            }
            CottonHarvestQuant.Text = quant.ToString();
            CottonHarvestQuant.Text = quant.ToString();
        }
        private void GuanoHarvestIncreaseQuant_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(GuanoHarvestQuant.Text) + 1;
            if (quant > environment.GuanoAmount)
            {
                quant = 0;
            }
            GuanoHarvestQuant.Text = quant.ToString();
            GuanoHarvestQuant.Text = quant.ToString();
        }
        private void GuanoHarvestDecreaseQuant_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(GuanoHarvestQuant.Text) - 1;
            if (quant < 0)
            {
                quant = Convert.ToInt32(GuanoHarvestPopulation.Text);
                GuanoHarvestQuant.Text = quant.ToString();
            }
            GuanoHarvestQuant.Text = quant.ToString();
        }
        private void CottonHarvestDecreaseQuant_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(CottonHarvestQuant.Text) - 1;
            if (quant < 0)
            {
                quant = Convert.ToInt32(CottonHarvestPopulation.Text);
                CottonHarvestQuant.Text = quant.ToString();
            }
            CottonHarvestQuant.Text = quant.ToString();
        }
        private void CornHarvestDecreaseQuant_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(CornHarvestQuant.Text) - 1;
            if (quant < 0)
            {
                quant = Convert.ToInt32(CornHarvestPopulation.Text);
                CornHarvestQuant.Text = quant.ToString();
            }
            CornHarvestQuant.Text = quant.ToString();
        }
        private void NextItemSell_Click(object sender, RoutedEventArgs e)
        {
            shopSellIndex++;
            if (shopSellIndex == Player.GetInstance().Inventory.Count)
            {
                shopSellIndex = 0;
            }
            LoadShop();
        }
        private void PreviousItemSell_Click(object sender, RoutedEventArgs e)
        {
            shopSellIndex--;
            if (shopSellIndex <= 0)
            {
                shopSellIndex = Player.GetInstance().Inventory.Count - 1;
            }
            LoadShop();
        }
        private void IncreaseQuantSell_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(SellQuant.Text) + 1;
            if (quant > Player.GetInstance().Inventory[shopSellIndex].Quantity)
            {
                quant = 0;
            }
            SellQuant.Text = quant.ToString();
            TotalPriceSell.Text = (quant * Player.GetInstance().Inventory[shopSellIndex].Value).ToString("c");
        }
        private void DecreaseQuantSell_Click(object sender, RoutedEventArgs e)
        {
            int quant = Convert.ToInt32(SellQuant.Text) - 1;
            if (quant < 0)
            {
                quant = Player.GetInstance().Inventory[shopSellIndex].Quantity;
                SellQuant.Text = quant.ToString();
            }
            SellQuant.Text = quant.ToString();
            TotalPriceSell.Text = (quant * Player.GetInstance().Inventory[shopSellIndex].Value).ToString("c");
        }
        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            Vender.GetInstance().Sell(Player.GetInstance().Inventory[shopSellIndex], SellQuant.Text);
            shopSellIndex = 0;
            LoadShop();
        }
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            EventOutput.Content += Vender.GetInstance().Buy(Vender.GetInstance().Inventory[shopBuyIndex], BuyQuantity.Text);
            shopBuyIndex = 0;
            LoadShop();
        }
        #endregion

        #region Events
        private void UpdatePlayerOnRatios(Entity entity)
        {
            entity.AmountChanged += entity.Entity_AmountChanged;
            
            if (entity.Status != "")
            {
                EventOutput.Content += $"\nDay {environment.Days}: {entity.Status}";
                entity.Status = "";
            }
        }
        #endregion

    }
}
