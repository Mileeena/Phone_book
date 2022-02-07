using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phone_book.Models;

namespace Phone_book.ContextFolder
{
    public class DataContext : DbContext
    {
        public DbSet<Сontact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"workstation id=online-db1.mssql.somee.com;
                                packet size=4096;
                                user id=MVT_Admin_SQLLogin_1;
                                pwd=opbcbywtsz;
                                data source=online-db1.mssql.somee.com;
                                persist security info=False;
                                initial catalog=online-db1"
            );
        }

        public Сontact GetContactById(int id)
        {
            return Contacts.FirstOrDefault(x => x.Id == id);
        }
    }
}
