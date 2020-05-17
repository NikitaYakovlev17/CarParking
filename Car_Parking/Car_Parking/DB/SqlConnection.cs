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
    class SqlConnect
    {
        private string sqlString = "Server=tcp:carparkingserver.database.windows.net,1433;Initial Catalog=car_parking_db;Persist Security Info=False;User ID=nikita;Password=375333587914Pmc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public SqlDataReader ReadUsersRecords()
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    string commandstr = "Select * From Users";
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


        public bool InsertUsersRecords(string phoneNumberLog, string password)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"INSERT INTO Users VALUES (@PhoneNumberLog, @Password, 0)";
                    command.Parameters.Add("@PhoneNumberLog", SqlDbType.NVarChar, 50);
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 50);

                    command.Parameters["@PhoneNumberLog"].Value = phoneNumberLog;
                    command.Parameters["@Password"].Value = password;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }


        public bool GiveUsersRecords(string phoneNumberLog, string password)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"Select count(*) From Users Where PhoneNumberLog = @PhoneNumberLog and Password = @Password";
                    command.Parameters.Add("@PhoneNumberLog", SqlDbType.NVarChar, 50);
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 50);

                    command.Parameters["@PhoneNumberLog"].Value = phoneNumberLog;
                    command.Parameters["@Password"].Value = password;
                    object count = command.ExecuteScalar();
                    if (Convert.ToInt32(count) > 0)
                    {
                        return true;
                    }
                    return false;
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
                    command.CommandText = @"Select UserId From Users Where PhoneNumberLog = @PhoneNumberLog";
                    command.Parameters.Add("@PhoneNumberLog", SqlDbType.NVarChar, 50);

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

        //public bool InsertCategory(string name)
        //{
        //    using (SqlConnection connect = new SqlConnection(sqlString))
        //    {
        //        try
        //        {
        //            connect.Open();
        //            SqlCommand command = new SqlCommand();
        //            command.Connection = connect;
        //            command.CommandText = @"INSERT INTO TaskTypes VALUES (@IdUser, @TaskTypeName)";
        //            command.Parameters.Add("@IdUser", SqlDbType.Int);
        //            command.Parameters.Add("@TaskTypeName", SqlDbType.NVarChar, 50);

        //            command.Parameters["@IdUser"].Value = Convert.ToInt32(Properties.Settings.Default.UserId);
        //            command.Parameters["@TaskTypeName"].Value = name;
        //            command.ExecuteNonQuery();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            return false;
        //        }
        //    }

        //}

        public bool DeleteCategory(string name)
        {
            using (SqlConnection connect = new SqlConnection(sqlString))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.CommandText = @"DELETE TaskTypes Where TaskTypeName=@TaskTypeName";
                    command.Parameters.Add("@TaskTypeName", SqlDbType.NVarChar, 50);

                    command.Parameters["@TaskTypeName"].Value = name;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }

        //public ObservableCollection<Class1> TakeCategoryById(int Id)
        //{
        //    using (SqlConnection connect = new SqlConnection(sqlString))
        //    {
        //        ObservableCollection<Class1> spam = new ObservableCollection<Class1>();
        //        try
        //        {
        //            connect.Open();
        //            SqlCommand command = new SqlCommand();
        //            command.Connection = connect;
        //            command.CommandText = @"Select TaskTypeName From TaskTypes Where IdUser = @IdUser";
        //            command.Parameters.Add("@IdUser", SqlDbType.Int);

        //            command.Parameters["@IdUser"].Value = Id;
        //            SqlDataReader info = command.ExecuteReader();

        //            string name;
        //            while (info.Read())
        //            {
        //                name = info["TaskTypeName"].ToString();
        //                spam.Add(new Class1() { Name = name });
        //            }
        //            return spam;
        //        }
        //        catch (Exception e)
        //        {
        //            return spam;
        //        }
        //    }

        //}
    }
}
