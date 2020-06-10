using Boo.Lang.Environments;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;


/**
* @Dennis Dupont & Rasmus Rosenkjær
* @1.4
* @5/6/2020
*/

public static class DatabaseManager
{


    /// <summary>
    /// Queries the database for Login Information
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    /// @02/5/2020
    /// <returns>Returns a Response Message, saying if the Login was Correct</returns>
    public static ResponseMessage QueryDatabaseForLogin(string username, string password)
    {
        //Opens the Connection to the Database, while we're in the production phase we're using the Root Login
        //Which will later be replaced with a safe user.
        using (MySqlConnection conn = new MySqlConnection("Server=172.16.21.167;port=3306;Database=SfPUser;UiD=root;Pwd=rd2020"))
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            string query = $"SELECT " +
                           $"Username, " +
                           $"AES_DECRYPT(AES_ENCRYPT('password','keystring'), 'keystring') " +
                           $"FROM User " +
                           $"WHERE Username='{username}' " +
                           $"AND AES_DECRYPT(AES_ENCRYPT('password','keystring'), 'keystring') = '{password}';";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.ExecuteNonQuery();


            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                return ResponseMessage.OK;
            else
                return ResponseMessage.Failed;
        }
    }


    /// <summary>
    /// Queries the database for a Users Personal Information
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    /// @Date 02/5/2020
    /// <returns>Returns a CitizenTemplate containing the Users Information</returns>
    public static CitizenTemplate QueryDatabaseForUserInfo(int userId)
    {
        //Opens the Connection to the Database, while we're in the production phase we're using the Root Login
        //Which will later be replaced with a safe user.
        using (MySqlConnection conn = new MySqlConnection("Server=172.16.21.168;port=3306;Database=UserLogin;UiD=root;Pwd=rd2020"))
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();


            string query = $"SELECT UserID, Firstname, Lastname, Zipcode FROM User WHERE UserID='{userId}'";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                CitizenTemplate citizen = new CitizenTemplate();
                citizen.UserID = reader.GetInt32(0);
                citizen.FirstName = reader.GetString(1);
                citizen.LastName = reader.GetString(2);
                citizen.ZipCode = reader.GetString(3);
                return citizen;
            }
            return null;
        }
    }


    /// <summary>
    /// This queries the server for a users location, status and time for the 60 latest entries
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    /// @Date 02/5/2020
    /// <returns>Returns an Array of </returns>
    public static CurrentLocationTemplate[] GetUserDataEntries(int userId)
    {
        //Opens the Connection to the Database, while we're in the production phase we're using the Root Login
        //Which will later be replaced with a safe user.
        using (MySqlConnection conn = new MySqlConnection("Server=172.16.21.169;port=3306;Database=AppData;UiD=root;Pwd=rd2020"))
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();


            string query = $"SELECT Status, Latitude, Longitude, CurrentTime, Act FROM CurrentLocation WHERE UserID='{userId}' ORDER BY UserID DESC LIMIT 60 ;";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            CurrentLocationTemplate[] LocationArray = new CurrentLocationTemplate[60];
            int i = 0;

            DataTable dt = new DataTable();
            dt.Load(reader);

            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow != null)
                {
                    CurrentLocationTemplate userLocationData = new CurrentLocationTemplate(dataRow["status"].ToString(), dataRow["latitude"].ToString(), dataRow["longitude"].ToString(), dataRow["currentTime"].ToString());
                    LocationArray[i++] = userLocationData;
                }
            }

            return LocationArray;
        }
    }


    /// <summary>
    /// This queries the server for a users status for the 60 latest entries
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    /// @Date 02/5/2020
    /// <returns>Returns a string with the Users status, based on the 60 latest entries</returns>
    public static string GetUserStatus(int userId)
    {
        //Opens the Connection to the Database, while we're in the production phase we're using the Root Login
        //Which will later be replaced with a safe user.
        using (MySqlConnection conn = new MySqlConnection("Server=172.16.21.169;port=3306;Database=AppData;UiD=root;Pwd=rd2020"))
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();


            string query = $"SELECT Status FROM CurrentLocation WHERE UserID='{userId}' ORDER BY UserID DESC LIMIT 60 ;";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            string status = "";
            int i = 0;

            DataTable dt = new DataTable();
            dt.Load(reader);

            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow != null)
                {
                    string str = dataRow["status"].ToString();

                    if (str == "Green" && status != "Yellow" && status != "Red")
                        status = "Green";
                    if (str == "Yellow" && status != "Red")
                        status = "Yellow";
                    if (str == "Red")
                        status = "Red";

                    i++;
                }
            }

            return status;
        }
    }


    /// <summary>
    /// This updates the status of the 60 latest entries in the Database,
    /// after a worker at SfP have acted upon them.
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    /// @Date 02/5/2020
    public static void UpdateStatus(int barId)
    {
        //Opens the Connection to the Database, while we're in the production phase we're using the Root Login
        //Which will later be replaced with a safe user.
        using (MySqlConnection conn = new MySqlConnection("Server=172.16.21.169;port=3306;Database=AppData;UiD=root;Pwd=rd2020"))
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();


            string query = $"UPDATE CurrentLocation SET Act = '1' WHERE (UserId = '{barId}' AND  Status = 'Red') OR (UserId = '{barId}' AND Status = 'yellow') ORDER BY ID DESC LIMIT 60;";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();


        }
    }
}
