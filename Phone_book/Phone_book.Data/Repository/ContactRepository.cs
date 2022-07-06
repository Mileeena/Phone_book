using Phone_book.Data;
using Phone_book.Data.Models;

namespace Phone_book.Data.Repository;

public class ContactRepository : IRepository<Contact>
{
    private readonly PhoneBookContext context;
    public IEnumerable<Contact> All => context.Contacts.ToList();

    public ContactRepository(PhoneBookContext context)
    {
        this.context = context;
    }

    public void Add(Contact entity)
    {
        context.Contacts.Add(entity);
        context.SaveChanges();
    }

    public void Delete(Contact entity)
    {
        context.Contacts.Remove(entity);
        context.SaveChanges();
    }

    public Contact FindById(int Id)
    {
        return context.Contacts.FirstOrDefault(x => x.Id == Id);
    }

    public void Update(Contact entity)
    {
        context.Contacts.Update(entity);
        context.SaveChanges();
    }
}