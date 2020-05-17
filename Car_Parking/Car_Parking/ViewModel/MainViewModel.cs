using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Input;



namespace Car_Parking.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }

        public ICommand logout => new DelegateCommand(LogoutCommand);
        public void LogoutCommand()
        {
            Properties.Settings.Default.User = "";
            Properties.Settings.Default.UserId = "";
            Properties.Settings.Default.Save();

            ViewLogin q = new ViewLogin();
            q.Show();
            CloseAction();
        }

        private string periodicity;
        public string Periodicity
        {
            get { return periodicity; }
            set
            {
                this.periodicity = value;
                RaisePropertiesChanged(nameof(Periodicity));
            }
        }
        private string userName;
        public string UserName
        {
            get { return getUserName(); }
            set
            {
                this.userName = value;
                RaisePropertiesChanged(nameof(UserName));
            }
        }
        private string getUserName()
        {
            return Properties.Settings.Default.User;
        }
        public object GridMain { get; private set; }
    }
}
