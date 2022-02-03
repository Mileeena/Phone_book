using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phone_book.Models
{
    public class Сontact
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public Сontact(int _id, string _surname, string _name,
            string _patronymic, string _phoneNumber, string _address)
        {
            Id = _id;
            Surname = _surname;
            Name = _name;
            Patronymic = _patronymic;
            PhoneNumber = _phoneNumber;
            Address = _address;
        }

        public Сontact(){}

    }
}
