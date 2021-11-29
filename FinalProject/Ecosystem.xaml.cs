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
        public Ecosystem()
        {
            InitializeComponent();

        }
        #endregion

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
            foreach (Entity e in environment.Entities)
            {
                //only update player about the entities that aren't the player and vendor
                if (e.Species.ToLower() != "human")
                    UpdatePlayerOnRatios(e);
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
        }
        #endregion

        private void GameLoop()
        {
            environment.Days++;
            if (environment.Days <= 273 & gamePause == false)
            {
                Bat.GetInstance().Eat();
                Bat.GetInstance().ProduceGuano();
                GuanoBeetle.GetInstance().Eat();
                environment.WeatherSystem.ChangeTemperature();
                if (environment.Days % 90 == 0)
                {
                    CornWorm.GetInstance().Reproduce();
                    CornWorm.GetInstance().Eat();
                    CottonWorm.GetInstance().Reproduce();
                    CottonWorm.GetInstance().Eat();
                    environment.WaterSupply -= Corn.GetInstance().GatherNutrients();
                    environment.WaterSupply -= Cotton.GetInstance().GatherNutrients();
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
            }
            else
            {
                ShopWindow.Visibility = Visibility.Hidden;
                Shop.Content = "Shop";
                State.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Events
        private void UpdatePlayerOnRatios(Entity entity)
        {
            entity.AmountChanged += entity.Entity_AmountChanged;

            if (entity.Status != "")
            {
                string current = EventOutput.Content.ToString();
                EventOutput.Content = $"Day {environment.Days}: {entity.Status}\n" + current;
            }
        }
        #endregion
    }
}
