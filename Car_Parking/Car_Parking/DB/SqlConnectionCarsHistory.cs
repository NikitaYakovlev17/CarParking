using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Car_Parking.Model;

namespace Car_Parking.DB
{
    class SqlConnectionCarsHistory
    {
        private string sqlString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public ObservableCollection<Car> ReadUsersRecords()
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                ObservableCollection<Car> spam = new ObservableCollection<Car>();

                try
                {
                    connect.Open();
                    string commandstr = "Select * From CarsHistory";
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = commandstr;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string car_number = reader["CarNumber"].ToString();
                            int car_region = Convert.ToInt32(reader["CarRegion"]);
                            string car_series = reader["CarSeries"].ToString();
                            DateTime lease_time = Convert.ToDateTime(reader["LeaseTime"]);
                            string phone_number = reader["PhoneNumber"].ToString();
                            string spaceType = reader["SpaceType"].ToString();
                            DateTime time_Out = Convert.ToDateTime(reader["TimeOut"]);
                            string pay_Amount= reader["PayAmount"].ToString();

                            spam.Add(new Car() { CarNumber = car_number, CarRegion = car_region, CarSeries = car_series, SpaceType = spaceType, LeaseTime = lease_time, TimeOut = time_Out, PhoneNumber = phone_number, PayAmount = pay_Amount });
                        }
                    }
                    return spam;
                }
                catch (Exception e)
                {
                    return spam;
                }
            }

        }


        public bool InsertUserCarRecords(string carNumber, int carRegion, string carSeries, DateTime leaseTime, string phoneNumber, string spaceType, DateTime timeOut, string payAmount)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"INSERT INTO CarsHistory VALUES (@CarNumber, @CarRegion, @CarSeries, @LeaseTime, @PhoneNumber, @SpaceType, @TimeOut, @PayAmount)";
                    command.Parameters.Add("@CarNumber", SqlDbType.NVarChar, 4);
                    command.Parameters.Add("@CarRegion", SqlDbType.Int, 2);
                    command.Parameters.Add("@CarSeries", SqlDbType.NVarChar, 2);
                    command.Parameters.Add("@LeaseTime", SqlDbType.DateTime);
                    command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 17);
                    command.Parameters.Add("@SpaceType", SqlDbType.NVarChar, 10);
                    command.Parameters.Add("@TimeOut", SqlDbType.DateTime);
                    command.Parameters.Add("@PayAmount", SqlDbType.NVarChar, 5);

                    command.Parameters["@CarNumber"].Value = carNumber;
                    command.Parameters["@CarRegion"].Value = carRegion;
                    command.Parameters["@CarSeries"].Value = carSeries;
                    command.Parameters["@LeaseTime"].Value = leaseTime;
                    command.Parameters["@PhoneNumber"].Value = phoneNumber;
                    command.Parameters["@SpaceType"].Value = spaceType;
                    command.Parameters["@TimeOut"].Value = timeOut;
                    command.Parameters["@PayAmount"].Value = payAmount;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }

        public ObservableCollection<Car> GiveUsersRecords(string phoneNumber)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                ObservableCollection<Car> spam = new ObservableCollection<Car>();

                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"Select * From CarsHistory Where PhoneNumber = @PhoneNumber";
                    command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 17);

                    command.Parameters["@PhoneNumber"].Value = phoneNumber;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string car_number = reader["CarNumber"].ToString();
                            int car_region = Convert.ToInt32(reader["CarRegion"]);
                            string car_series = reader["CarSeries"].ToString();
                            DateTime lease_time = Convert.ToDateTime(reader["LeaseTime"]);
                            string phone_number = reader["PhoneNumber"].ToString();
                            string spaceType = reader["SpaceType"].ToString();
                            DateTime time_out = Convert.ToDateTime(reader["TimeOut"]);
                            string pay_amount = reader["PayAmount"].ToString();

                            spam.Add(new Car() { CarNumber = car_number, CarRegion = car_region, CarSeries = car_series, SpaceType = spaceType, LeaseTime = lease_time, TimeOut = time_out, PhoneNumber = phone_number, PayAmount = pay_amount });
                        }
                    }
                    return spam;


                }
                catch (Exception e)
                {
                    return spam;
                }
            }

        }
    }
}
