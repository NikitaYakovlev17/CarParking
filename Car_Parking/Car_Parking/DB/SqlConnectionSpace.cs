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
    class SqlConnectionSpace
    {
        private string sqlString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

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

        public string GetPayAmountBySpace(string spaceType)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"Select PayAmount From ParkingSpace Where SpaceType = @SpaceType";
                    command.Parameters.Add("@SpaceType", SqlDbType.NVarChar, 50);

                    command.Parameters["@SpaceType"].Value = spaceType;
                    SqlDataReader info = command.ExecuteReader();
                    object id = -1;
                    while (info.Read())
                    {
                        id = info["PayAmount"];
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
    }
}
