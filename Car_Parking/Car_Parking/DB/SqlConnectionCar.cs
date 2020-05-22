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
    class SqlConnectionCar
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
                    string commandstr = "Select * From UserCar";
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
                            string spaceType = reader["SpaceType"].ToString();
                            DateTime lease_time = Convert.ToDateTime(reader["LeaseTime"]);
                            string phone_number = reader["PhoneNumber"].ToString();

                            spam.Add(new Car() { CarNumber = car_number, CarRegion = car_region, CarSeries = car_series, SpaceType = spaceType, LeaseTime = lease_time, TimeOut = DateTime.Now, PhoneNumber = phone_number});
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

        public bool InsertUserCarRecords(string carNumber, int carRegion, string carSeries, DateTime leaseTime, string phoneNumber, string spaceType)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"INSERT INTO UserCar VALUES (@IdUser, @CarNumber, @CarRegion, @CarSeries, @LeaseTime, @PhoneNumber, @SpaceType)";
                    command.Parameters.Add("@IdUser", SqlDbType.Int);
                    command.Parameters.Add("@CarNumber", SqlDbType.NVarChar, 4);
                    command.Parameters.Add("@CarRegion", SqlDbType.Int, 2);
                    command.Parameters.Add("@CarSeries", SqlDbType.NVarChar, 2);
                    command.Parameters.Add("@LeaseTime", SqlDbType.DateTime);
                    command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 17); 
                    command.Parameters.Add("@SpaceType", SqlDbType.NVarChar, 10);

                    command.Parameters["@IdUser"].Value = Convert.ToInt32(Properties.Settings.Default.UserId);
                    command.Parameters["@CarNumber"].Value = carNumber;
                    command.Parameters["@CarRegion"].Value = carRegion;
                    command.Parameters["@CarSeries"].Value = carSeries;
                    command.Parameters["@LeaseTime"].Value = leaseTime;
                    command.Parameters["@PhoneNumber"].Value = phoneNumber;
                    command.Parameters["@SpaceType"].Value = spaceType;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }

        public string GetIdUserByName(string phoneNumberLog)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"Select UserId From UserCar Where PhoneNumberLog = @PhoneNumberLog";
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50);

                    command.Parameters["@PhoneNumberLog"].Value = phoneNumberLog;
                    SqlDataReader info = command.ExecuteReader();
                    object id = -1;
                    while (info.Read())
                    {
                        id = info["UserId"];
                        break;
                    }
                    return Convert.ToString(id);
                }
                catch (Exception e)
                {
                    return "";
                }
            }

        }

        public ObservableCollection<Car> GiveUsersRecords(string carNumber, int carRegion, string carSeries)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                ObservableCollection<Car> spam = new ObservableCollection<Car>();

                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"Select * From UserCar Where CarNumber = @CarNumber and CarRegion = @CarRegion and Carseries = @CarSeries";
                    command.Parameters.Add("@CarNumber", SqlDbType.NVarChar, 4);
                    command.Parameters.Add("@CarRegion", SqlDbType.Int, 1);
                    command.Parameters.Add("@CarSeries", SqlDbType.NVarChar, 2);

                    command.Parameters["@CarNumber"].Value = carNumber;
                    command.Parameters["@CarRegion"].Value = carRegion;
                    command.Parameters["@CarSeries"].Value = carSeries;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string car_number = reader["CarNumber"].ToString();
                            int car_region = Convert.ToInt32(reader["CarRegion"]);
                            string car_series = reader["CarSeries"].ToString();
                            string spaceType = reader["SpaceType"].ToString();
                            DateTime lease_time = Convert.ToDateTime(reader["LeaseTime"]);
                            string phone_number = reader["PhoneNumber"].ToString();

                            spam.Add(new Car() { CarNumber = car_number, CarRegion = car_region, CarSeries = car_series, SpaceType = spaceType, LeaseTime = lease_time, TimeOut = DateTime.Now, PhoneNumber = phone_number});
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


        public bool DeleteCar(string carNumber, int carRegion, string carSeries)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"Delete From UserCar Where CarNumber = @CarNumber and CarRegion = @CarRegion and Carseries = @CarSeries";
                    command.Parameters.Add("@CarNumber", SqlDbType.NVarChar, 4);
                    command.Parameters.Add("@CarRegion", SqlDbType.Int, 1);
                    command.Parameters.Add("@CarSeries", SqlDbType.NVarChar, 2);

                    command.Parameters["@CarNumber"].Value = carNumber;
                    command.Parameters["@CarRegion"].Value = carRegion;
                    command.Parameters["@CarSeries"].Value = carSeries;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }
    }
}
