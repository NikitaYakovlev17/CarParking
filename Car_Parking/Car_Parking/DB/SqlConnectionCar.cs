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

namespace Car_Parking.DB
{
    class SqlConnectionCar
    {
        private string sqlString = "Server=tcp:carparkingserver.database.windows.net,1433;Initial Catalog=car_parking_db;Persist Security Info=False;User ID=nikita;Password=375333587914Pmc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public SqlDataReader ReadUsersRecords()
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    string commandstr = "Select * From UserCar";
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = commandstr;
                    SqlDataReader info = command.ExecuteReader();
                    return info;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

        }

        public bool InsertUserCarRecords(int carNumber, int carRegion, string carSeries, DateTime leaseTime, int phoneNumber, string comment)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"INSERT INTO UserCar VALUES (@IdUser, @CarNumber, @CarRegion, @CarSeries, @LeaseTime, @PhoneNumber, @Comment)";
                    command.Parameters.Add("@IdUser", SqlDbType.Int);
                    command.Parameters.Add("@CarNumber", SqlDbType.Int, 4);
                    command.Parameters.Add("@CarRegion", SqlDbType.Int, 2);
                    command.Parameters.Add("@CarSeries", SqlDbType.NVarChar, 2);
                    command.Parameters.Add("@LeaseTime", SqlDbType.DateTime);
                    command.Parameters.Add("@PhoneNumber", SqlDbType.Int, 7);
                    command.Parameters.Add("@Comment", SqlDbType.NVarChar, 100);

                    command.Parameters["@IdUser"].Value = Convert.ToInt32(Properties.Settings.Default.UserId);
                    command.Parameters["@CarNumber"].Value = carNumber;
                    command.Parameters["@CarRegion"].Value = carRegion;
                    command.Parameters["@CarSeries"].Value = carSeries;
                    command.Parameters["@LeaseTime"].Value = leaseTime;
                    command.Parameters["@PhoneNumber"].Value = phoneNumber;
                    command.Parameters["@Comment"].Value = comment;
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
