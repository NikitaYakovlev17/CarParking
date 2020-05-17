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
    class SqlConnectionTime
    {
        private string sqlString = "Server=tcp:carparkingserver.database.windows.net,1433;Initial Catalog=car_parking_db;Persist Security Info=False;User ID=nikita;Password=375333587914Pmc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public SqlDataReader ReadUsersRecords()
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    string commandstr = "Select * From LeaseTime";
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

        public bool InsertLeaseTimeRecords(DateTime leaseTime, DateTime timeOut, int payAmount)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"INSERT INTO LeaseTime VALUES (@LeaseTime, @Time_Out, @PayAmount)";

                    command.Parameters.Add("@LeaseTime", SqlDbType.DateTime);
                    command.Parameters.Add("@Time_Out", SqlDbType.DateTime);
                    command.Parameters.Add("@PayAmount", SqlDbType.Int);

                    command.Parameters["@LeaseTime"].Value = leaseTime;
                    command.Parameters["@Time_Out"].Value = timeOut;
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
    }
}
