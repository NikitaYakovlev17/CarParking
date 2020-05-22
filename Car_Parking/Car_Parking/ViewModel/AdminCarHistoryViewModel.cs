using Car_Parking.DB;
using Car_Parking.Model;
using DevExpress.Mvvm;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Car_Parking.ViewModel
{
    class AdminCarHistoryViewModel : ViewModelBase
    {
        private string phonenumber;
        public string PhoneNumber
        {
            get { return phonenumber; }
            set
            {
                if (value == this.phonenumber) return;
                this.phonenumber = value;
                PhoneMask();
                RaisePropertiesChanged(nameof(PhoneNumber));
            }
        }

        public int PhoneLength { get; set; }

        public async Task PhoneMask()
        {
            var newVal = Regex.Replace(PhoneNumber, @"[^0-9]", "");
            if (PhoneLength != newVal.Length && !string.IsNullOrEmpty(newVal))
            {
                PhoneLength = newVal.Length;
                PhoneNumber = string.Empty;

                if (newVal.Length <= 3)
                {
                    PhoneNumber = Regex.Replace(newVal, @"(375)", "+$1");
                }
                else if (newVal.Length <= 5)
                {
                    PhoneNumber = Regex.Replace(newVal, @"(375)(\d{0,2})", "+$1($2)");
                }
                else if (newVal.Length <= 8)
                {
                    PhoneNumber = Regex.Replace(newVal, @"(375)(\d{2})(\d{0,3})", "+$1($2)$3");
                }
                else if (newVal.Length <= 10)
                {
                    PhoneNumber = Regex.Replace(newVal, @"(375)(\d{2})(\d{0,3})(\d{0,2})", "+$1($2)$3-$4");
                }
                else if (newVal.Length > 10)
                {
                    PhoneNumber = Regex.Replace(newVal, @"(375)(\d{2})(\d{0,3})(\d{0,2})(\d{0,2})", "+$1($2)$3-$4-$5");
                }
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

        public AdminCarHistoryViewModel()
        {
            CarsCollection = AllItems();
        }

        private ObservableCollection<Car> AllItems()
        {
            SqlConnectionCarsHistory spam = new SqlConnectionCarsHistory();
            ObservableCollection<Car> carsDB = spam.ReadUsersRecords();
            CarsCollection = new ObservableCollection<Car>();
            if (carsDB != null)
            {
                foreach (var item in carsDB)
                    CarsCollection.Add(item);
            }
            return CarsCollection;
        }


        private ObservableCollection<Car> GetProducts()
        {
            SqlConnectionCarsHistory spam = new SqlConnectionCarsHistory();
            ObservableCollection<Car> carsDB = spam.GiveUsersRecords(PhoneNumber);
            CarsCollection = new ObservableCollection<Car>();
            if (carsDB != null)
            {
                foreach (var item in carsDB)
                    CarsCollection.Add(item);
            }
            return CarsCollection;
        }

        public ICommand search => new DelegateCommand(SearchCommand);
        public void SearchCommand()
        {
            bool flagToAccept = true;
            flag = true;
            ErrorMes = "";
            if(PhoneNumber != null)
            {
                if (PhoneNumber == String.Empty || PhoneNumber.Length != 17)
                {
                    flagToAccept = false;
                    ErrorMes = Properties.Resources.phoneNumberEmpty;
                }
            }
            if(flagToAccept)
            {
                CarsCollection = GetProducts();
                PhoneNumber = null;
            }
        }

        public ICommand all_cars => new DelegateCommand(AllCarsCommand);
        public void AllCarsCommand()
        {
            CarsCollection = AllItems();
        }

        bool flag = false;

        private string errorMes;
        public string ErrorMes
        {
            get { return errorMes; }
            set
            {
                this.errorMes = value;
                RaisePropertiesChanged(nameof(ErrorMes));
            }
        }

    }
}
