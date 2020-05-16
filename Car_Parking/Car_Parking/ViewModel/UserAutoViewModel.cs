using Car_Parking.DB;
using DevExpress.Mvvm;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Car_Parking.ViewModel
{
    class UserAutoViewModel : ViewModelBase, IDataErrorInfo
    {
        private int carnumber;
        public int CarNumber
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

        private DateTime leasetime;
        public DateTime LeaseTime
        {
            get { return leasetime; }
            set
            {
                this.leasetime = value;
                RaisePropertiesChanged(nameof(LeaseTime));
            }
        }

        private int phonenumber;
        public int PhoneNumber
        {
            get { return phonenumber; }
            set
            {
                this.phonenumber = value;
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


        public ICommand accept => new DelegateCommand(AcceptCommand);
        public void AcceptCommand()
        {
            bool flagToAccept = true;
            bool IsDone = true;
            flag = true;
            ErrorMes = "";
            if(CarNumber < 1000 || CarNumber > 9999)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.carNumberEmpty;
            }
            if(CarRegion < 1 || CarRegion > 7)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.carRegionEmpty;
            }
            if(CarSeries == String.Empty || CarSeries == null)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.carSeriesEmpty;
            }
            if(LeaseTime == null)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.leaseTimeEmpty;
            }
            if(PhoneNumber < 10000000 || PhoneNumber > 99999999)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.phoneNumberEmpty;
            }
            if (flagToAccept)
            {
                SqlConnectionCar spam = new SqlConnectionCar();
                spam.InsertUserCarRecords(CarNumber, CarRegion, CarSeries, LeaseTime, PhoneNumber, Comment);
            }
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
