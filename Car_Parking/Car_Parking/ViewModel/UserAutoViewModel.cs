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
using System.Text.RegularExpressions;

namespace Car_Parking.ViewModel
{
    class UserAutoViewModel : ViewModelBase, IDataErrorInfo
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

        private DateTime leasetime = DateTime.Now;
        public DateTime LeaseTime
        {
            get { return leasetime; }
            set
            {
                this.leasetime = value;
                RaisePropertiesChanged(nameof(LeaseTime));
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

        private int payAmount;
        public int PayAmount
        {
            get { return payAmount; }
            set
            {
                this.payAmount = value;
                RaisePropertiesChanged(nameof(PayAmount));
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
                PhoneMask();
                RaisePropertiesChanged(nameof(PhoneNumber));
            }
        }

        private string spaceType;
        public string SpaceType
        {
            get { return spaceType; }
            set
            {
                this.spaceType = value;
                RaisePropertiesChanged(nameof(SpaceType));
            }
        }


        public int PhoneLength { get; set; }

        public void PhoneMask()
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
                PhoneLength = 0;
            }
        }
                


        public ICommand accept => new DelegateCommand(AcceptCommand);
        public void AcceptCommand()
        {
            bool flagToAccept = true;
            bool IsDone = true;
            flag = true;
            ErrorMes = "";
            if(CarNumber == null || CarNumber.Length != 4)
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
            if(SpaceType == null || SpaceType == String.Empty)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.carSpaceEmpty;
            }
            if (LeaseTime == null)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.leaseTimeEmpty;
            }
            if(PhoneNumber == null || PhoneNumber == String.Empty || PhoneNumber.Length != 17)
            {
                flagToAccept = false;
                ErrorMes = Properties.Resources.phoneNumberEmpty;
            }
            if (flagToAccept)
            {
                SqlConnectionCar spam = new SqlConnectionCar();
                spam.InsertUserCarRecords(CarNumber, CarRegion, CarSeries, LeaseTime, PhoneNumber, SpaceType);
                CarNumber = String.Empty;
                CarRegion = 0;
                CarSeries = String.Empty;
                LeaseTime = DateTime.Now;
                PhoneNumber = String.Empty;
                SpaceType = String.Empty;
                PhoneLength = 0;
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
