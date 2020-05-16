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
using System.Windows.Shapes;
using Car_Parking.ViewModel;

namespace Car_Parking
{
    /// <summary>
    /// Логика взаимодействия для ViewLogin.xaml
    /// </summary>
    public partial class ViewLogin : Window
    {
        public ViewLogin()
        {
            LoginViewModel vm = new LoginViewModel();
            this.DataContext = vm;
            if (vm.CloseAction2 == null)
                vm.CloseAction2 = new Action(this.Close);
            InitializeComponent();
        }
    }
}
