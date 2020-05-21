using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Parking.Model
{
    class Car
    {
        public string CarNumber { get; set; }
        public int CarRegion { get; set; }
        public string CarSeries { get; set; }
        public DateTime LeaseTime { get; set; }
        public DateTime TimeOut { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public string PayAmount { get; set; }

        public Car(string carNumber, int carRegion, string carSeries, DateTime leaseTime, string phoneNumber, string comment)
        {
            this.CarNumber = carNumber;
            this.CarRegion = carRegion;
            this.CarSeries = carSeries;
            this.LeaseTime = leaseTime;
            this.PhoneNumber = phoneNumber;
            this.Comment = comment;
        }

        public Car() { }
    }
}
