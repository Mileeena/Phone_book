using Microsoft.EntityFrameworkCore;
using Phone_book.Data.Models;

namespace Phone_book.Data;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public PhoneBookContext(DbContextOptions<PhoneBookContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}