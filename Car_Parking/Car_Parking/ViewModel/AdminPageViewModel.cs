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
    class AdminPageViewModel : ViewModelBase, IDataErrorInfo
    {
        private string carnumber;
        public string CarNumber
        {
            get { return carnumber; }
            set
            {
                this.carnumber = value;
                RaisePropertiesChanged(nameof(CarNumber));
            }
        }

        private int carregion;
        public int CarRegion
        {
            get { return carregion; }
            set
            {
                this.carregion = value;
                RaisePropertiesChanged(nameof(CarRegion));
            }
        }

        private string carseries;
        public string CarSeries
        {
            get { return carseries; }
            set
            {
                this.carseries = value;
                RaisePropertiesChanged(nameof(CarSeries));
            }
        }

        private DateTime leaseTime;
        public DateTime LeaseTime
        {
            get { return leaseTime; }
            set
            {
                this.leaseTime = value;
                RaisePropertiesChanged(nameof(leaseTime));
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                this.phoneNumber = value;
                RaisePropertiesChanged(nameof(PhoneNumber));
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                this.comment = value;
                RaisePropertiesChanged(nameof(Comment));
            }
        }

        private string payAmount;
        public string PayAmount
        {
            get { return payAmount; }
            set
            {
                this.payAmount = value;
                RaisePropertiesChanged(nameof(PayAmount));
            }
        }

        private bool EnableDone;
        public bool IsEnableDone
        {
            get { return EnableDone; }
            set
            {
                this.EnableDone = value;
                RaisePropertiesChanged(nameof(IsEnableDone));
            }
        }

        private DateTime timeOut = DateTime.Now;
        public DateTime TimeOut
        {
            get { return timeOut; }
            set
            {
                this.timeOut = value;
                RaisePropertiesChanged(nameof(TimeOut));
            }
        }

        private TimeSpan timeOn;
        public TimeSpan TimeOn
        {
            get { return timeOn; }
            set
            {
                this.timeOn = value;
                RaisePropertiesChanged(nameof(TimeOn));
            }
        }

        public TimeSpan GetTimeOut()
        {

            var leaseTime = (from time in CarsCollection select time.LeaseTime);
            DateTime leaseTime2;
            foreach (var time in leaseTime)
            {
                leaseTime2 = time;
                DateTime now = DateTime.Now;
                TimeOn = now.Subtract(leaseTime2);
            }

            return TimeOn;
        }

        public ICommand accept_admin => new DelegateCommand(AcceptCommand);

        public void AcceptCommand()
        {
            bool flagToAccept = true;
            flag = true;
            ErrorMes = "";
            if (CarNumber != null)
            {
                if (CarNumber.Length != 4 || CarNumber == String.Empty)
                {
                    flagToAccept = false;
                    ErrorMes = Properties.Resources.carNumberEmpty;
                }
            }
            else 
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.carNumberEmpty;
            }
            if (CarRegion < 1 || CarRegion > 7 || CarNumber == String.Empty)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.carRegionEmpty;
            }
            if (CarSeries == String.Empty || CarSeries == null)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.carSeriesEmpty;
            }
            if (flagToAccept)
            {
                SqlConnectionCar spam = new SqlConnectionCar();
                PayAmount = payAmount_func();
                CarsCollection = GetProducts();                
                TimeOn = GetTimeOut();
                IsEnableDone = true;                
            }
        }

        public string payAmount_func()
        {
            var leaseTime = (from time in CarsCollection select time.LeaseTime);
            DateTime leaseTime2;
            foreach (var time in leaseTime)
            {
                leaseTime2 = time;
                DateTime timeOut = DateTime.Now;
                TimeSpan t = timeOut.Subtract(leaseTime2);
                int q = Convert.ToInt32(t.TotalHours);
                PayAmount = Convert.ToString((q + 1) * 2 +"$");               
            }

            return PayAmount;
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

        public AdminPageViewModel()
        {
            CarsCollection = AllItems();
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

        private ObservableCollection<Car> GetProducts()
        {
            SqlConnectionCar spam = new SqlConnectionCar();
            ObservableCollection<Car> carsDB = spam.GiveUsersRecords(CarNumber, CarRegion, CarSeries);
            CarsCollection = new ObservableCollection<Car>();
            if (carsDB != null)
            {
                foreach (var item in carsDB)
                    CarsCollection.Add(item);
            }
            return CarsCollection;
        }

        
        public ICommand delete_car_admin => new DelegateCommand(DeleteComand);

        public void DeleteComand()
        {
            var leaseTime = (from item in CarsCollection select item.LeaseTime);
            foreach (var time in leaseTime)
                LeaseTime = time;
            var phoneNumber = (from item in CarsCollection select item.PhoneNumber);
            foreach (var number in phoneNumber)
                PhoneNumber = number;
            var comment = (from item in CarsCollection select item.Comment);
            foreach (var com in comment)
                Comment = com;
            PayAmount = payAmount_func();
            SqlConnectionCarsHistory history = new SqlConnectionCarsHistory();
            history.InsertUserCarRecords(CarNumber, CarRegion, CarSeries, LeaseTime, PhoneNumber, Comment, TimeOut, PayAmount);
            SqlConnectionCar spam = new SqlConnectionCar();
            spam.DeleteCar(CarNumber, CarRegion, CarSeries);
            CarsCollection = AllItems();
            CarNumber = String.Empty;
            CarRegion = 0;
            CarSeries = String.Empty;
            PayAmount = String.Empty;
            TimeOn = TimeSpan.Zero;
        }


        #region Validation
        bool flag = false;
        public string Error { get { return null; } }

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;
                if (flag)
                {
                    switch (columnName)
                    {
                        case "Login":
                            break;
                    }
                }
                return result;
            }
        }

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

        #endregion
    }
}
