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
        public string SpaceType { get; set; }
        public DateTime LeaseTime { get; set; }
        public DateTime TimeOut { get; set; }
        public string PhoneNumber { get; set; }
        public string PayAmount { get; set; }

        public Car(string carNumber, int carRegion, string carSeries, string spaceType, DateTime leaseTime, string phoneNumber)
        {
            this.CarNumber = carNumber;
            this.CarRegion = carRegion;
            this.CarSeries = carSeries;
            this.SpaceType = spaceType;
            this.LeaseTime = leaseTime;
            this.PhoneNumber = phoneNumber;
        }

        public Car() { }
    }
}
