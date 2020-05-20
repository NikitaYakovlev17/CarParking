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
using Car_Parking.View;
using System.Configuration;
using Car_Parking.DB;
using Car_Parking.ViewModel;
using System.IO;

namespace Car_Parking
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var setting = new ConnectionStringSettings
            {
                Name = "ConnectionString",
                ConnectionString = @"Server=tcp:carparkingserver.database.windows.net,1433;Initial Catalog=car_parking_db;Persist Security Info=False;User ID=nikita;Password=375333587914Pmc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
            };

            Configuration config;
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Add(setting);
            config.Save();

            if (Properties.Settings.Default.User == "")
            {
                SqlConnect l = new SqlConnect();
                l.ReadUsersRecords();
                ViewLogin taskWindow = new ViewLogin();
                taskWindow.Show();
                this.Close();
            }

            MainViewModel vm = new MainViewModel();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
            InitializeComponent();
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;

        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "UserHome":
                    Main.Navigate(new UserHome());
                    break;
                case "UserAuto":
                    Main.Navigate(new UserAuto());
                    break;
                case "AdminPage":
                    Main.Navigate(new AdminPage());
                    break;
                default:
                    break;
            }

        }
    }
}
