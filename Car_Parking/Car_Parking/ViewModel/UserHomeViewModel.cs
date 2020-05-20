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
    class UserHomeViewModel : ViewModelBase
    {

        private int valueIOT;
        public int ValueIOT
        {
            get { return valueIOT; }
            set
            {
                this.valueIOT = value;
                RaisePropertiesChanged(nameof(ValueIOT));
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

        public UserHomeViewModel()
        {
            CarsCollection = AllItems();
            ValueIOT = CarsCollection.Count();
        }
        private ObservableCollection<Car> AllItems()
        {
            SqlConnectionCar spam = new SqlConnectionCar();
            ObservableCollection<Car> carsDB = spam.ReadUsersRecords();
            CarsCollection = new ObservableCollection<Car>();
            if (carsDB != null)
            {
                foreach (var item in carsDB)
                    CarsCollection.Add(item);
            }
            return CarsCollection;
        }
    }
}
