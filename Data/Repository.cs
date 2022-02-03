using Phone_book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace Phone_book.Data
{
    public static class Repository
    {
        public static List<Сontact> Contacts = new List<Сontact>();
        public static List<Сontact> GetСontacts()
        {
            Сontact temp;

            MySqlConnectionStringBuilder strConnection = new MySqlConnectionStringBuilder()
            {
                //для локального сервера
                //Server = "localhost",
                //UserID = "root",
                //Port = 3306,
                //Database = "pb_db",
                //Password = "Sa25092002"

                //для сервера на https://www.db4free.net/
                Server = "db4free.net",
                UserID = "root_55",
                Port = 3306,
                Database = "db_contacts_mysq",
                Password = "qwer1234"
            };
            try
            {
                MySqlConnection db = new MySqlConnection(strConnection.ConnectionString);
                db.Open();
                    //var SelectAll = @"SELECT * FROM pb_db.contacts;";
                    var SelectAll = @"SELECT * FROM db_contacts_mysq.contacts;";

                MySqlCommand selectAllCommand = new MySqlCommand(SelectAll, db);
                    MySqlDataReader reader = selectAllCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        temp = new Сontact(
                            (int)reader["Id"],
                            reader["Surname"].ToString(),
                            reader["Name"].ToString(),
                            reader["Patronymic"].ToString(),
                            reader["Phone_Number"].ToString(),
                            reader["Address"].ToString());

                        Contacts.Add(temp);
                        
                }
            }
            catch (Exception e)
            {

            }
            return Contacts;
        }

        public static Сontact GetContactById(int id)
        { 
            return Contacts.FirstOrDefault(x => x.Id == id);
        }
    }
}
