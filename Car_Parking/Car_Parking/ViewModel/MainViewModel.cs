using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Input;
using Car_Parking.DB;
using Car_Parking.Model;
using System.Globalization;
using WPFLocalizeExtension.Engine;
using System.Collections.ObjectModel;

namespace Car_Parking.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            PhoneNumber = Properties.Settings.Default.User;
            SqlConnectionCar record = new SqlConnectionCar();
            ObservableCollection<Car> carsDB = record.GiveUsersRecordsByPhoneNumber(PhoneNumber);
            CarsCollection = new ObservableCollection<Car>();
            if (carsDB != null)
            {
                foreach (var item in carsDB)
                    CarsCollection.Add(item);
            }
            if(CarsCollection.Count == 0)
            {
                ButtonVisability = false;
            }
            else
            {
                ButtonVisability = true;
            }


            SqlConnect spam = new SqlConnect();
            bool admin = spam.IsAdminById();
            if (admin)
            {
                PanelVisability = true;
                ButtonVisability = true;
            }
        }

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

        public ICommand Language => new DelegateCommand(Set_Language);
        private int q = 0;

        private void Set_Language()
        {
            if (q == 0)
            {
                q++;
                LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
                LocalizeDictionary.Instance.Culture = new CultureInfo("en");
            }
            else
            {
                q--;
                LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
                LocalizeDictionary.Instance.Culture = new CultureInfo("");
            }
        }

        private string phonenumber;
        public string PhoneNumber
        {
            get { return phonenumber; }
            set
            {
                if (value == this.phonenumber) return;
                this.phonenumber = value;                
                RaisePropertiesChanged(nameof(PhoneNumber));
            }
        }

        private ObservableCollection<Car> carsCollection;
        public ObservableCollection<Car> CarsCollection
        {
            get
            {
                return carsCollection;
            }
            set
            {
                this.carsCollection = value;
                RaisePropertiesChanged(nameof(CarsCollection));
            }
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

        private bool panelVisability;
        public bool PanelVisability
        {
            get { return panelVisability; }
            set
            {
                this.panelVisability = value;
                RaisePropertiesChanged(nameof(PanelVisability));
            }
        }

        private bool buttonVisability;
        public bool ButtonVisability
        {
            get { return buttonVisability; }
            set
            {
                this.buttonVisability = value;
                RaisePropertiesChanged(nameof(ButtonVisability));
            }
        }


        private string getUserName()
        {
            SqlConnect spam = new SqlConnect();
            bool admin = spam.IsAdminById();
            if(admin)
            {
                UserName = "Admin";
            }
            else
            {
                UserName = "User";
            }
            return userName;
        }
        public object GridMain { get; private set; }
    }
}
