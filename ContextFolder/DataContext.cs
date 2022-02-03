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
                @"Server=DESKTOP\SQLEXPRESS;
                                Database=local_contacts_db;
                                Trusted_Connection=true;"
            );
        }

        public Сontact GetContactById(int id)
        {
            return Contacts.FirstOrDefault(x => x.Id == id);
        }
    }
}
