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

        private int valueA;
        public int ValueA
        {
            get { return valueA; }
            set
            {
                this.valueA = value;
                RaisePropertiesChanged(nameof(ValueA));
            }
        }

        private int valueB;
        public int ValueB
        {
            get { return valueB; }
            set
            {
                this.valueB = value;
                RaisePropertiesChanged(nameof(ValueB));
            }
        }

        private int valueC;
        public int ValueC
        {
            get { return valueC; }
            set
            {
                this.valueC = value;
                RaisePropertiesChanged(nameof(ValueC));
            }
        }

        private int valueD;
        public int ValueD
        {
            get { return valueD; }
            set
            {
                this.valueD = value;
                RaisePropertiesChanged(nameof(ValueD));
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
            int CountA = (from item in CarsCollection where item.SpaceType.Equals("Сектор A") select item).Count();
            int CountB = (from item in CarsCollection where item.SpaceType.Equals("Сектор B") select item).Count();
            int CountC = (from item in CarsCollection where item.SpaceType.Equals("Сектор C") select item).Count();
            int CountD = (from item in CarsCollection where item.SpaceType.Equals("Сектор D") select item).Count();
            ValueA = CountA;
            ValueB = CountB;
            ValueC = CountC;
            ValueD = CountD;
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
