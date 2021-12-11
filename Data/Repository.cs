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
        public static List<Сontact> GetСontact()
        {
            List<Сontact> Contacts = new List<Сontact>();
            Сontact temp;
            //localhost;
            //user id = root; port = 3306; persistsecurityinfo = False; database = pb_db

            //string connectStr= "server=localhost;user=root;port=3306;database = pb_db;password=Sa25092002;";

            MySqlConnectionStringBuilder strConnection = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                UserID = "root",
                Port = 3306,
                Database = "pb_db",
                Password = "Sa25092002"
            };
            try
            {
                MySqlConnection db = new MySqlConnection(strConnection.ConnectionString);
                db.Open();
                    var SelectAll = @"SELECT * FROM pb_db.contacts;";

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


            //return new Сontact()
            //{
            //    Id = 1,
            //    Surname = "surname",
            //    Name = "name",
            //    Patronymic = "Витальевич",
            //    PhoneNumber = "79990863742",
            //    Address = "KnA"
            //};

            // Сontact(int Id, string Surname, string Name, string Patronymic, string PhoneNumber, string Address)
            //{
            //    this.Id = Id;
            //    this.Surname = Surname;
            //    this.Name = Name;
            //    this.Patronymic = Patronymic;
            //    this.PhoneNumber = PhoneNumber;
            //    this.Address = Address;

            //}
            return Contacts;
        }
    }
}
