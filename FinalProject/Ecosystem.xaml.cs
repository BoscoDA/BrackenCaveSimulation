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
        Environment environment = Environment.GetInstance();
        DispatcherTimer timer;
        TimeSpan timeSpan;
        public bool gamePause = false;
        public Ecosystem()
        {
            InitializeComponent();

        }
        private void LoadInformation()
        {
            CornName.DataContext = environment.Entities[0];
            CottonName.DataContext = environment.Entities[1];
            BatName.DataContext = Bat.GetInstance();
            HawkName.DataContext = environment.Entities[5];
            CornWormName.DataContext = CornWorm.GetInstance();
            CottonWormName.DataContext = environment.Entities[3];
            GuanoBeetleName.DataContext = environment.Entities[7];
            DermestidBeetleName.DataContext = environment.Entities[6];
            CornPopulation.Text = Corn.GetInstance().Population.ToString();
            CottonPopulation.Text = Cotton.GetInstance().Population.ToString();
            BatPopulation.Text = Bat.GetInstance().Population.ToString();
            HawkPopulation.Text = Hawk.GetInstance().Population.ToString();
            CornWormPopulation.Text = CornWorm.GetInstance().Population.ToString();
            CottonWormPopulation.Text = CottonWorm.GetInstance().Population.ToString();
            GuanoBeetlePopulation.Text = GuanoBeetle.GetInstance().Population.ToString();
            DermestidBeetlePopulation.Text = DermestidBeetle.GetInstance().Population.ToString();
            EnvironmentName.Text = environment.Name;
            EnvironmentGuanoAmountOuput.Text = $"Guano Amount: {environment.GuanoAmount}";
            EnvironmentTemperatureOuput.Text = $"Temperature: {environment.Temperature} °F";
            EnvironmentWaterSupplyOuput.Text = $"Water Supply: {environment.WaterSupply}%";
            foreach (Entity e in environment.Entities)
            {
                //only update player about the entities that aren't the player and vendor
                if (e.Species.ToLower() != "human")
                    UpdatePlayerOnRatios(e);
            }
        }
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

        }

        private void UpdatePlayerOnRatios(Entity entity)
        {
            entity.AmountChanged += entity.Entity_AmountChanged;

            if (entity.Status != "")
            {
                string current = EventOutput.Content.ToString();
                EventOutput.Content = $"Day {environment.Days}: {entity.Status}\n" + current;
            }
        }
    }
}
